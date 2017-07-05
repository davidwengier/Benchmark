using System;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;

namespace Benchmark
{
	[MemoryDiagnoser]
	[RPlotExporter]
	public class StringsToCsv
	{
		private byte[] data;

		[Params(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 100, 1000)]
		public int Iterations { get; set; }

		[GlobalSetup]
		public void Setup()
		{
			data = new byte[this.Iterations];
			new Random().NextBytes(data);
		}

		[Benchmark(Baseline = true)]
		public string StringConcatenation()
		{
			string result = "";
			foreach (byte datum in data)
			{
				result += datum + ",";
			}
			result = result.Remove(result.Length - 1);
			return result;
		}

		[Benchmark]
		public string StringBuilder()
		{
			StringBuilder sb = new StringBuilder();
			foreach (byte datum in data)
			{
				sb.Append(datum);
				sb.Append(',');
			}
			sb.Remove(sb.Length - 1, 1);
			return sb.ToString();
		}

		[Benchmark]
		public string StringBuilder_WithCapacity()
		{
			StringBuilder sb = new StringBuilder(data.Length * 2);
			foreach (byte datum in data)
			{
				sb.Append(datum);
				sb.Append(',');
			}
			sb.Remove(sb.Length - 1, 1);
			return sb.ToString();
		}

		[Benchmark]
		public string StringJoin()
		{
			return string.Join(",", data);
		}

		[Benchmark]
		public string Aggregate()
		{
			return data.Aggregate(string.Empty, (current, next) => current + (next + ","));
		}
	}
}