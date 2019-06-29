using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;

namespace BenchmarkFull
{
    [MemoryDiagnoser]
    [RPlotExporter]
    public class LoopVsLinq
    {
        private readonly string[] parameters = new[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" };

        [Benchmark]
        public string Iterative()
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                string parameter = parameters[i];
                if (string.Equals(parameter, "Five", StringComparison.OrdinalIgnoreCase))
                {
                    return parameter;
                }
            }
            return null;
        }

        [Benchmark]
        public string LINQ_FirstOrDefault()
        {
            return parameters.FirstOrDefault(p => p.Equals("Five", StringComparison.OrdinalIgnoreCase));
        }

        [Benchmark]
        public string LINQ_Where()
        {
            return parameters.Where(p => p.Equals("Five", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }
    }
}