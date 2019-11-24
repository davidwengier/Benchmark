using BenchmarkDotNet.Attributes;

namespace BenchmarkCore
{
    // From: https://twitter.com/mattleibow/status/1198721425785466883
    [MemoryDiagnoser]
    public class InterpolatedStrings
    {
        [Benchmark]
        public string IntToString()
        {
            return $"b{1.ToString()}b";
        }

        [Benchmark]
        public string Int()
        {
            return $"a{1}a";
        }
    }
}
