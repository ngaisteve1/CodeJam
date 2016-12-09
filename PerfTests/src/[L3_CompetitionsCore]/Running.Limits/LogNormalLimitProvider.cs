﻿using System;
using System.Linq;

using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Reports;

using JetBrains.Annotations;

namespace CodeJam.PerfTests.Running.Limits
{
	/// <summary>
	/// Competition limit provider for microbenchmarks. Uses lognormal distribution for results estimation.
	/// </summary>
	/// <seealso cref="CompetitionLimitProviderBase"/>
	[PublicAPI]
	public class LogNormalLimitProvider : CompetitionLimitProviderBase
	{
		/// <summary>Instance of the provider.</summary>
		public static readonly ICompetitionLimitProvider Instance = new LogNormalLimitProvider();

		/// <summary>
		/// Prevents a default instance of the <see cref="LogNormalLimitProvider"/> class from being created.
		/// </summary>
		private LogNormalLimitProvider() { }

		/// <summary>Short description for the provider.</summary>
		/// <value>The short description for the provider.</value>
		public override string ShortInfo => "Lnml";

		/// <summary>Limits for the benchmark.</summary>
		/// <param name="baselineReport">The baseline report.</param>
		/// <param name="benchmarkReport">The benchmark report.</param>
		/// <param name="limitMode">If <c>true</c> limit values should be returned. Actual values returned otherwise.</param>
		/// <returns>Limits for the benchmark or empty range if none.</returns>
		protected override LimitRange TryGetCompetitionLimit(
			BenchmarkReport baselineReport,
			BenchmarkReport benchmarkReport,
			bool limitMode)
		{
			var baselineStat = new Statistics(
				baselineReport.GetResultRuns()
					.Select(r => r.GetAverageNanoseconds())
					.Select(r => r <= 0 ? 0 : Math.Log(r)));
			var benchmarkStat = new Statistics(
				benchmarkReport.GetResultRuns()
					.Select(r => r.GetAverageNanoseconds())
					.Select(r => r <= 0 ? 0 : Math.Log(r)));

			var minValueBaseline = Math.Exp(baselineStat.Mean);
			var maxValueBaseline = Math.Exp(baselineStat.Mean);

			// ReSharper disable CompareOfFloatsByEqualityOperator
			if (minValueBaseline == 0 || maxValueBaseline == 0)
				// ReSharper restore CompareOfFloatsByEqualityOperator
				return LimitRange.Empty;

			var minValueBenchmark = Math.Exp(benchmarkStat.Mean);
			var maxValueBenchmark = Math.Exp(benchmarkStat.Mean);

			var minRatio = minValueBenchmark / minValueBaseline;
			var maxRatio = maxValueBenchmark / maxValueBaseline;

			if (limitMode)
			{
				minRatio *= 0.98; // 0.99*0.99 accuracy
				maxRatio *= 1.02; // 1.01*1.01 accuracy
			}
			return LimitRange.CreateRatioLimit(
				Math.Min(minRatio, maxRatio),
				maxRatio);
		}
	}
}