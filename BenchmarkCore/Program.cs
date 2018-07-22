using BenchmarkDotNet.Running;
using System;

namespace BenchmarkCore
{
    public class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<SubstringVsSpan>();
        }
    }
}