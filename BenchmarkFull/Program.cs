using System;
using System.Linq;
using BenchmarkDotNet.Running;

namespace BenchmarkFull
{
	public class Program
	{
		public static void Main(string[] args)
		{
            BenchmarkRunner.Run<SubstringVsSpan>();
            //BenchmarkRunner.Run<GetCurrentMethod>();
            //BenchmarkRunner.Run<StringsToCsv>();
            //BenchmarkRunner.Run<Exceptions>();
            //BenchmarkRunner.Run<LoopVsLinq>();
            //BenchmarkRunner.Run<DictionaryVsIDictionary>();
        }
	}
}