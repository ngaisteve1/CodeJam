using System.Collections.Generic;

using CodeJam.PerfTests.Configs;

using NUnit.Framework;

namespace CodeJam.PerfTests.IntegrationTests
{
	[TestFixture(Category = "BenchmarkDotNet")]
	[CompetitionBurstMode]
	[CompetitionMeasureAll]
	public class CompetitionAllMetricsTest
	{
		private const int Count = 1024;

		[Test]
		public void RunCompetitionAllMetricsTest() => Competition.Run(this);

		[CompetitionBaseline]
		[GcAllocations(8.24, 8.27, BinarySizeUnit.Kilobyte), Gc0(0), Gc1(0), Gc2(0)]
		[ExpectedTime(4.84, 14.45, TimeUnit.Microsecond)]
		public int ListWithoutCapacity()
		{
			var data = new List<int>();
			for (int i = 0; i < Count; i++)
				data.Add(i);
			return data.Count;
		}

		[CompetitionBenchmark(0.33, 1.13)]
		[GcAllocations(4.08, BinarySizeUnit.Kilobyte), Gc0(0), Gc1(0), Gc2(0)]
		[ExpectedTime(3.90, 8.15, TimeUnit.Microsecond)]
		public int ListWithCapacity()
		{
			var data = new List<int>(Count);
			for (int i = 0; i < Count; i++)
				data.Add(i);
			return data.Count;
		}

		[CompetitionBenchmark(0.097, 0.27)]
		[GcAllocations(4.03, 4.05, BinarySizeUnit.Kilobyte), Gc0(0), Gc1(0), Gc2(0)]
		[ExpectedTime(1.44, 2.44, TimeUnit.Microsecond)]
		public int Array()
		{
			var data = new int[Count];
			for (int i = 0; i < Count; i++)
				data[i] = i;
			return data.Length;
		}
	}
}