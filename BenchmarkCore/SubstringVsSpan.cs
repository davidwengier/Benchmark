using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;

namespace BenchmarkCore
{
    [RPlotExporter]
    [MemoryDiagnoser]
    public class SubstringVsSpan
    {
        private readonly string _plate = "123456789012436784";

        [Params(5, 10, 15)]
        public int Number { get; set; }

        [Benchmark]
        public long Span()
        {
            var span = _plate.AsSpan();
            for (int i = 0; i < Number; i++)
            {
                span = span.Slice(1);
            }
            return long.Parse(span);
        }

        [Benchmark]
        public long Substring()
        {
            var tmp = _plate;
            for (int i = 0; i < Number; i++)
            {
                tmp = tmp.Substring(1);
            }
            return long.Parse(tmp);
        }
    }
}