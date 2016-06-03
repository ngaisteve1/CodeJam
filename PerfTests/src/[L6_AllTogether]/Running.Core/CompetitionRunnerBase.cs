﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Helpers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Toolchains.InProcess;
using BenchmarkDotNet.Validators;

using CodeJam.PerfTests.Analysers;
using CodeJam.PerfTests.Configs;
using CodeJam.PerfTests.Loggers;
using CodeJam.PerfTests.Running.Messages;

using JetBrains.Annotations;

namespace CodeJam.PerfTests.Running.Core
{
	/// <summary>Base class for competition benchmark runners</summary>
	[PublicAPI]
	[SuppressMessage("ReSharper", "ArrangeRedundantParentheses")]
	[SuppressMessage("ReSharper", "SuggestVarOrType_BuiltInTypes")]
	[SuppressMessage("ReSharper", "VirtualMemberNeverOverriden.Global")]
	public abstract class CompetitionRunnerBase
	{
		/// <summary>Basic host logger implementation</summary>
		/// <seealso cref="CodeJam.PerfTests.Loggers.HostLogger" />
		protected abstract class HostLogger : Loggers.HostLogger
		{
			/// <summary>Initializes a new instance of the <see cref="HostLogger"/> class.</summary>
			/// <param name="wrappedLogger">The logger to redirect the output. Cannot be null.</param>
			/// <param name="logMode">Host logging mode.</param>
			protected HostLogger(ILogger wrappedLogger, HostLogMode logMode)
				: base(wrappedLogger, logMode) { }
		}

		#region Default values
		// DONTTOUCH: update xml docs for ICompetitionConfig.MaxRunsAllowed after changing the constant.
		private const int DefaultMaxRunsAllowed = 10;

		private static readonly TimeSpan _tooFastRunLimit = new TimeSpan(4); // 400 ns

		// DONTTOUCH: update xml docs for ICompetitionConfig.AllowLongRunningBenchmarks after changing the value.
		private static readonly TimeSpan _longRunLimit = TimeSpan.FromMilliseconds(500); // 500 ms
		private static readonly TimeSpan _allowLongRunLimit = TimeSpan.FromDays(1);
		#endregion

		/// <summary>Runs the benchmark.</summary>
		/// <typeparam name="T">Type of the benchmark class.</typeparam>
		/// <param name="thisReference">Reference used to enable type inference.</param>
		/// <param name="competitionConfig">The competition config.</param>
		/// <returns>State of the competition.</returns>
		public CompetitionState Run<T>(T thisReference, ICompetitionConfig competitionConfig) where T : class =>
			RunCompetition(thisReference.GetType(), competitionConfig);

		/// <returns>State of the competition.</returns>
		public CompetitionState Run<T>(ICompetitionConfig competitionConfig) where T : class =>
			RunCompetition(typeof(T), competitionConfig);

		public virtual CompetitionState RunCompetition(Type benchmarkType, ICompetitionConfig competitionConfig)
		{
			competitionConfig = competitionConfig ?? DefaultCompetitionConfig.Instance;

			ValidateCompetitionSetup(benchmarkType, competitionConfig);
			OnBeforeRun(benchmarkType, competitionConfig);

			var benchmarkConfig = CreateBenchmarkConfig(competitionConfig);
			var maxRunsAllowed = competitionConfig.MaxRunsAllowed;
			if (maxRunsAllowed <= 0)
			{
				maxRunsAllowed = DefaultMaxRunsAllowed;
			}

			bool runSucceed = false;
			Summary summary = null;
			CompetitionState competitionState;
			try
			{
				competitionState = CompetitionCore.Run(benchmarkType, benchmarkConfig, maxRunsAllowed);
				summary = competitionState.LastRunSummary;
				runSucceed = true;

				ProcessRunComplete(competitionState, competitionConfig);
			}
			finally
			{
				OnAfterRun(runSucceed, benchmarkConfig, summary);

				var hostLogger = benchmarkConfig.GetLoggers().OfType<HostLogger>().Single();
				ReportHostLogger(hostLogger, summary);
			}

			ReportMessagesToUser(competitionState, competitionConfig);

			DebugCode.AssertState(competitionState.Completed, "Bug: competition state does not mark as completed.");

			return competitionState;
		}

		private void ValidateCompetitionSetup(Type benchmarkType, ICompetitionConfig competitionConfig)
		{
			if (!competitionConfig.AllowDebugBuilds && !EnvironmentInfo.GetCurrent().HasAttachedDebugger)
			{
				var assembly = benchmarkType.Assembly;
				if (assembly.IsDebugAssembly())
					throw new InvalidOperationException(
						$"Set the solution configuration into Release mode. Assembly {assembly.GetName().Name} was build as debug.");

				foreach (var referencedAssemblyName in assembly.GetReferencedAssemblies())
				{
					var refAssembly = Assembly.Load(referencedAssemblyName);
					if (refAssembly.IsDebugAssembly())
						throw new InvalidOperationException(
							$"Set the solution configuration into Release mode. Assembly {refAssembly.GetName().Name} was build as debug.");
				}
			}
		}

		private void ProcessRunComplete(CompetitionState competitionState, ICompetitionConfig competitionConfig)
		{
			if (competitionConfig.DetailedLogging && competitionState?.Logger != null)
			{
				var logger = competitionState.Logger;
				var messages = competitionState.GetMessages();

				if (messages.Length == 0)
				{
					logger.WriteLine();
					logger.WriteLineInfo("// No messages in run.");
				}
				else
				{
					logger.WriteLine();
					logger.WriteLineInfo("// All messages:");
					foreach (var message in messages)
					{
						logger.LogMessage(message);
					}
				}
			}
		}

		private void ReportMessagesToUser(CompetitionState competitionState, ICompetitionConfig competitionConfig)
		{
			if (competitionState == null)
				return;

			var criticalErrorMessages = GetMessageLines(
				competitionState, m => m.MessageSeverity > MessageSeverity.TestError, false);
			bool hasCriticalMessages = criticalErrorMessages.Length > 0;

			var testFailedMessages = GetMessageLines(
				competitionState, m => m.MessageSeverity == MessageSeverity.TestError, hasCriticalMessages);
			bool hasTestFailedMessages = testFailedMessages.Length > 0;

			var warningMessages = GetMessageLines(
				competitionState, m => m.MessageSeverity == MessageSeverity.Warning, hasCriticalMessages);
			bool hasWarnings = warningMessages.Length > 0;

			var infoMessages = GetMessageLines(
				competitionState, m => m.MessageSeverity < MessageSeverity.Warning, hasCriticalMessages);
			bool hasInfo = infoMessages.Length > 0;

			if (!(hasCriticalMessages || hasTestFailedMessages || hasWarnings))
				return;

			var allMessages = new List<string>();

			// TODO: simplify
			if (hasCriticalMessages)
			{
				allMessages.Add("Execution failed, details below.");
			}
			else if (hasTestFailedMessages)
			{
				allMessages.Add("Test failed, details below.");
			}
			else
			{
				allMessages.Add("Test completed with warnings, details below.");
			}

			if (hasCriticalMessages)
			{
				allMessages.Add("Errors:");
				allMessages.AddRange(criticalErrorMessages);
			}
			if (hasTestFailedMessages)
			{
				allMessages.Add("Failed assertions:");
				allMessages.AddRange(testFailedMessages);
			}
			if (hasWarnings)
			{
				allMessages.Add("Warnings:");
				allMessages.AddRange(warningMessages);
			}
			if (hasInfo)
			{
				allMessages.Add("Diagnostic messages:");
				allMessages.AddRange(infoMessages);
			}

			var messageText = string.Join(Environment.NewLine, allMessages);
			if (hasCriticalMessages)
			{
				ReportExecutionErrors(messageText);
			}
			else if (hasTestFailedMessages || competitionConfig.ReportWarningsAsErrors)
			{
				ReportAssertionsFailed(messageText);
			}
			else
			{
				ReportWarnings(messageText);
			}
		}

		private bool ShouldReport(IMessage message, CompetitionState competitionState) =>
			message.RunNumber == competitionState.RunNumber ||
				(message.MessageSource != MessageSource.Analyser && message.MessageSource != MessageSource.Diagnoser);

		private string[] GetMessageLines(CompetitionState competitionState, Func<IMessage, bool> filter, bool fromAllRuns)
		{
			var result =
				from message in competitionState.GetMessages()
				where fromAllRuns || ShouldReport(message, competitionState)
				where filter(message)
				orderby
					message.MessageSource,
					(int)message.MessageSeverity descending,
					message.RunNumber,
					message.RunMessageNumber
				select $"    * Run #{message.RunNumber}: {message.MessageText}";

			return result.ToArray();
		}

		#region Host-related logic
		protected virtual void OnBeforeRun(Type benchmarkType, ICompetitionConfig config) { }
		protected virtual void OnAfterRun(bool runSucceed, IConfig benchmarkConfig, Summary summary) { }

		protected abstract HostLogger CreateHostLogger(HostLogMode hostLogMode);
		protected abstract void ReportHostLogger(HostLogger logger, [CanBeNull] Summary summary);

		protected abstract void ReportExecutionErrors(string messages);
		protected abstract void ReportAssertionsFailed(string messages);
		protected abstract void ReportWarnings(string messages);
		#endregion

		#region Create benchark config
		private IConfig CreateBenchmarkConfig(ICompetitionConfig competitionConfig)
		{
			var result = new ManualConfig();

			result.Add(OverrideJobs(competitionConfig).ToArray());
			result.Add(OverrideExporters(competitionConfig).ToArray());
			result.Add(OverrideDiagnosers(competitionConfig).ToArray());

			result.Add(GetLoggers(competitionConfig).ToArray());
			result.Add(GetColumns(competitionConfig).ToArray());
			result.Add(GetValidators(competitionConfig).ToArray());
			result.Add(GetAnalysers(competitionConfig).ToArray());

			return result.AsReadOnly();
		}

		private List<ILogger> GetLoggers(ICompetitionConfig competitionConfig)
		{
			var result = OverrideLoggers(competitionConfig);
			var hostLogger = CreateHostLogger(
				competitionConfig.DetailedLogging ? HostLogMode.AllMessages : HostLogMode.PrefixedOnly);
			result.Insert(0, hostLogger);
			return result;
		}

		private List<IColumn> GetColumns(ICompetitionConfig competitionConfig)
		{
			var result = OverrideColumns(competitionConfig);
			result.AddRange(
				new[]
				{
					StatisticColumn.Min,
					BaselineDiffColumn.Scaled50,
					BaselineDiffColumn.Scaled85,
					BaselineDiffColumn.Scaled95,
					StatisticColumn.Max
				});

			return result;
		}

		private List<IValidator> GetValidators(ICompetitionConfig competitionConfig)
		{
			var result = OverrideValidators(competitionConfig);
			if (competitionConfig.GetJobs().Any(j => j.Toolchain is InProcessToolchain))
			{
				// ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
				if (competitionConfig.AllowDebugBuilds || !competitionConfig.UpdateSourceAnnotations)
				{
					result.Insert(0, InProcessValidator.DontFailOnError);
				}
				else
				{
					result.Insert(0, InProcessValidator.FailOnError);
				}
			}

			if (competitionConfig.AllowDebugBuilds || EnvironmentInfo.GetCurrent().HasAttachedDebugger)
			{
				result.RemoveAll(v => v is JitOptimizationsValidator);
			}
			else
			{
				if (result.All(v => !(v is JitOptimizationsValidator)))
				{
					result.Insert(0, JitOptimizationsValidator.FailOnError);
				}
			}
			result.Insert(0, new RunStateSlots());

			return result;
		}

		private List<IAnalyser> GetAnalysers(ICompetitionConfig competitionConfig)
		{
			var result = OverrideAnalysers(competitionConfig);

			// DONTTOUCH: the CompetitionAnnotateAnalyser should be last analyser in the chain.
			if (!competitionConfig.DontCheckAnnotations)
			{
				var annotator = new CompetitionAnnotateAnalyser
				{
					TooFastBenchmarkLimit = _tooFastRunLimit,
					LongRunningBenchmarkLimit = competitionConfig.AllowLongRunningBenchmarks ? _allowLongRunLimit : _longRunLimit,
					IgnoreExistingAnnotations = competitionConfig.IgnoreExistingAnnotations,
					LogCompetitionLimits = competitionConfig.LogCompetitionLimits,
					UpdateSourceAnnotations = competitionConfig.UpdateSourceAnnotations,
					PreviousRunLogUri = competitionConfig.PreviousRunLogUri
				};

				if (competitionConfig.RerunIfLimitsFailed)
				{
					annotator.MaxRerunsIfValidationFailed = 3;
					annotator.AdditionalRerunsOnAnnotate = 2;
				}

				result.Add(annotator);
			}

			return result;
		}

		#region Override config parameters
		protected virtual List<IJob> OverrideJobs(ICompetitionConfig baseConfig) =>
			baseConfig.GetJobs().ToList();

		protected virtual List<IExporter> OverrideExporters(ICompetitionConfig baseConfig) =>
			baseConfig.GetExporters().ToList();

		protected virtual List<IDiagnoser> OverrideDiagnosers(ICompetitionConfig baseConfig) =>
			baseConfig.GetDiagnosers().ToList();

		protected virtual List<ILogger> OverrideLoggers(ICompetitionConfig baseConfig) =>
			baseConfig.GetLoggers().ToList();

		protected virtual List<IColumn> OverrideColumns(ICompetitionConfig baseConfig) =>
			baseConfig.GetColumns().ToList();

		protected virtual List<IValidator> OverrideValidators(ICompetitionConfig baseConfig) =>
			baseConfig.GetValidators().ToList();

		protected virtual List<IAnalyser> OverrideAnalysers(ICompetitionConfig baseConfig) =>
			baseConfig.GetAnalysers().ToList();
		#endregion

		#endregion
	}
}