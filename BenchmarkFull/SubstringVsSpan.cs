using BenchmarkDotNet.Attributes;
using System;
using System.Linq;

namespace BenchmarkFull
{
    [MemoryDiagnoser]
    [RPlotExporter]
    public class SubstringVsSpan
    {
        private string plate = "12345678901243";

        [Params(1, 5, 10)]
        public int Number { get; set; }

        [Benchmark]
        public long Span()
        {
            var span = plate.AsSpan();
            for (int i = 0; i < Number; i++)
            {
                span = span.Slice(1);
            }
            return long.Parse(span.ToString());
        }

        [Benchmark]
        public long Substring()
        {
            var tmp = plate;
            for (int i = 0; i < Number; i++)
            {
                tmp = tmp.Substring(1);
            }
            return long.Parse(tmp);
        }
    }
}