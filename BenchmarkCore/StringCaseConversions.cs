using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;

namespace BenchmarkCore
{
    [MemoryDiagnoser]
    [RPlotExporter]
    public class StringCaseConversions
    {
        private readonly string _input = "iTs an OlDer memE bUt IT cHEcKS Out!";

        [Benchmark]
        public string ToLower()
        {
            return _input.ToLower();
        }

        [Benchmark]
        public string ToLowerInvariant()
        {
            return _input.ToLowerInvariant();
        }

        [Benchmark]
        public string ToUpper()
        {
            return _input.ToUpper();
        }

        [Benchmark]
        public string ToUpperInvariant()
        {
            return _input.ToUpperInvariant();
        }
    }
}
