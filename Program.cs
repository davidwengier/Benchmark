using System;
using System.Linq;
using BenchmarkDotNet.Running;

namespace Benchmark
{
	public class Program
	{
		public static void Main(string[] args)
		{
			BenchmarkRunner.Run<StringsToCsv>();
			BenchmarkRunner.Run<Exceptions>();
			BenchmarkRunner.Run<LoopVsLinq>();
			BenchmarkRunner.Run<DictionaryVsIDictionary>();
		}
	}
}