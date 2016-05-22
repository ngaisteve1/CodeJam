﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

using BenchmarkDotNet.Competitions;
using BenchmarkDotNet.Helpers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Running.Competitions.Core;
using BenchmarkDotNet.Running.Competitions.SourceAnnotations;
using BenchmarkDotNet.Running.Messages;

namespace BenchmarkDotNet.Analysers
{
	[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
	[SuppressMessage("ReSharper", "SuggestVarOrType_BuiltInTypes")]
	internal class CompetitionLimitsAnalyser : IAnalyser
	{
		#region Competition targets
		// TODO: to readonly dict + api to update the targets?
		protected class CompetitionTargets : Dictionary<MethodInfo, CompetitionTarget> { }

		protected static readonly RunState<CompetitionTargets> TargetsSlot =
			new RunState<CompetitionTargets>();
		#endregion

		#region Properties
		public bool AllowSlowBenchmarks { get; set; }
		public bool IgnoreExistingAnnotations { get; set; }
		public int MaxRuns { get; set; }

		public CompetitionLimit DefaultCompetitionLimit { get; set; }
		#endregion

		public IEnumerable<IWarning> Analyse(Summary summary)
		{
			var warnings = new List<IWarning>();
			ValidatePreconditions(summary, warnings);

			if (warnings.Count == 0)
			{
				var competitionTargets = TargetsSlot[summary];
				if (competitionTargets.Count == 0)
				{
					InitCompetitionTargets(competitionTargets, summary, warnings);
				}

				ValidateSummary(summary, competitionTargets, warnings);

				ValidatePostconditions(summary, warnings);
			}

			var result = warnings.ToArray();
			CompetitionCore.FillAnalyserMessages(summary, warnings);
			return result;
		}

		#region Parsing competition target info
		protected virtual void InitCompetitionTargets(
			CompetitionTargets competitionTargets,
			Summary summary,
			List<IWarning> warnings)
		{
			competitionTargets.Clear();

			var resourceCache = new Dictionary<string, XDocument>();
			foreach (var target in summary.GetTargets())
			{
				if (warnings.Count > 0)
					break;

				var competitionAttribute = target.Method.GetCustomAttribute<CompetitionBenchmarkAttribute>();
				if (competitionAttribute != null &&
					!competitionAttribute.Baseline &&
					!competitionAttribute.DoesNotCompete)
				{
					var competitionTarget = GetCompetitionTarget(
						target, competitionAttribute,
						resourceCache, warnings);

					competitionTargets.Add(target.Method, competitionTarget);
				}
			}
		}

		private CompetitionTarget GetCompetitionTarget(
			Target target, CompetitionBenchmarkAttribute competitionAttribute,
			IDictionary<string, XDocument> resourceCache,
			List<IWarning> warnings)
		{
			var fallbackLimit = DefaultCompetitionLimit ?? CompetitionLimit.Empty;

			var targetResourceName = AttributeAnnotations.TryGetTargetResourceName(target);
			if (targetResourceName == null)
			{
				if (IgnoreExistingAnnotations)
					return new CompetitionTarget(target, fallbackLimit, false);

				return AttributeAnnotations.ParseCompetitionTarget(target, competitionAttribute);
			}

			// DONTTOUCH: the doc should be loaded for validation even if IgnoreExistingAnnotations = true
			XDocument resourceDoc;
			if (!resourceCache.TryGetValue(targetResourceName, out resourceDoc))
			{
				resourceDoc = XmlAnnotations.TryLoadResourceDoc(target, targetResourceName, warnings);
				resourceCache[targetResourceName] = resourceDoc;
			}

			if (resourceDoc == null || IgnoreExistingAnnotations)
				return new CompetitionTarget(target, fallbackLimit, true);

			var result = XmlAnnotations.TryParseCompetitionTarget(target, resourceDoc);
			return result ??
				new CompetitionTarget(target, fallbackLimit, true);
		}
		#endregion

		#region Validation
		protected virtual void ValidateSummary(
			Summary summary, CompetitionTargets competitionTargets,
			List<IWarning> warnings)
		{
			var validated = true;
			foreach (var benchmarkGroup in summary.SameConditionBenchmarks())
			{
				foreach (var benchmark in benchmarkGroup)
				{
					CompetitionTarget competitionTarget;
					if (!competitionTargets.TryGetValue(benchmark.Target.Method, out competitionTarget))
						continue;

					validated &= ValidateBenchmark(
						summary, benchmark,
						competitionTarget, warnings);
				}
			}

			var competitionState = CompetitionCore.RunState[summary];
			if (!validated && MaxRuns > competitionState.RunNumber)
			{
				// TODO: detailed message???
				competitionState.RequestReruns(1, "Competition validation failed.");
			}
		}

		private bool ValidateBenchmark(
			Summary summary, Benchmark benchmark,
			CompetitionLimit competitionLimit, List<IWarning> warnings)
		{
			if (benchmark.Target.Baseline)
				return true;

			const int percentile = 95;

			var targetMethodName = benchmark.Target.Method.Name;
			var actualRatio = summary.TryGetScaledPercentile(benchmark, percentile);
			if (actualRatio == null)
			{
				var baselineBenchmark = summary.TryGetBaseline(benchmark);
				warnings.AddWarning(
					MessageSeverity.SetupError,
					$"Baseline benchmark {baselineBenchmark?.ShortInfo} does not compute",
					summary.TryGetBenchmarkReport(benchmark));
				return false;
			}

			bool validated = true;

			var actualRatioText = actualRatio.GetValueOrDefault().ToString(
				CompetitionLimitConstants.ActualRatioFormat,
				EnvironmentInfo.MainCultureInfo);

			if (!competitionLimit.IgnoreMin)
			{
				if (actualRatio < competitionLimit.Min)
				{
					validated = false;
					warnings.AddWarning(
						MessageSeverity.TestError,
						$"Method {targetMethodName} runs faster than {competitionLimit.MinText}x baseline. Actual ratio: {actualRatioText}x",
						summary.TryGetBenchmarkReport(benchmark));
				}
			}

			if (!competitionLimit.IgnoreMax)
			{
				if (actualRatio > competitionLimit.Max)
				{
					validated = false;
					warnings.AddWarning(
						MessageSeverity.TestError,
						$"Method {targetMethodName} runs slower than {competitionLimit.MaxText}x baseline. Actual ratio: {actualRatioText}x",
						summary.TryGetBenchmarkReport(benchmark));
				}
			}

			return validated;
		}

		// ReSharper disable once MemberCanBeMadeStatic.Local
		private void ValidatePreconditions(Summary summary, List<IWarning> warnings)
		{
			if (summary.HasCriticalValidationErrors)
			{
				warnings.AddWarning(
					MessageSeverity.ExecutionError,
					"Summary has validation errors.",
					null);
			}

			if (warnings.Count == 0)
			{
				var reportedBenchmarks = new HashSet<Benchmark>(
					summary.Reports
						.Where(r => r.ExecuteResults.Any())
						.Select(r => r.Benchmark));

				var benchMissing = summary.Benchmarks
					.Where(b => !reportedBenchmarks.Contains(b))
					.Select(b => b.Target.Method.Name)
					.Distinct()
					.ToArray();

				if (benchMissing.Length > 0)
				{
					warnings.AddWarning(
						MessageSeverity.ExecutionError,
						"No reports for benchmarks: " + string.Join(", ", benchMissing));
				}
			}
		}

		private void ValidatePostconditions(Summary summary, List<IWarning> warnings)
		{
			var tooFastReports = summary.Reports
				.Where(rp => rp.GetResultRuns().Any(r => r.GetAverageNanoseconds() < 400))
				.Select(rp => rp.Benchmark.Target.Method.Name)
				.ToArray();
			if (tooFastReports.Length > 0)
			{
				warnings.AddWarning(
					MessageSeverity.Warning,
					"The benchmarks " + string.Join(", ", tooFastReports) +
						" runs faster than 400 nanoseconds. Results cannot be trusted.");
			}

			if (!AllowSlowBenchmarks)
			{
				var tooSlowReports = summary.Reports
					.Where(rp => rp.GetResultRuns().Any(r => r.GetAverageNanoseconds() > 500 * 1000 * 1000))
					.Select(rp => rp.Benchmark.Target.Method.Name)
					.ToArray();

				if (tooSlowReports.Length > 0)
				{
					warnings.
						AddWarning(
							MessageSeverity.Warning,
							"The benchmarks " + string.Join(", ", tooSlowReports) +
								" runs longer than half a second. Consider to rewrite the test as the peek timings will be hidden by averages" +
								$" or set the {nameof(AllowSlowBenchmarks)} to true.");
				}
			}
		}
		#endregion
	}
}