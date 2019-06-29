using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;

namespace BenchmarkFull
{
    [MemoryDiagnoser]
    [RPlotExporter]
    public class Exceptions
    {
        private readonly Random _random = new Random();
        private string _data;

        [GlobalSetup]
        public void Setup()
        {
            int num = _random.Next(1, 11);
            _data = num > 5 ? "asdf" : num.ToString();
        }

        [Benchmark]
        public int Parse()
        {
            try
            {
                return int.Parse(_data);
            }
            catch
            {
                return -1;
            }
        }

        [Benchmark]
        public int TryParse()
        {
            if (int.TryParse(_data, out int result))
            {
                return result;
            }
            return 0;
        }
    }
}