using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace BenchmarkCore
{
    [MemoryDiagnoser]
    public class Collections
    {
        private ImmutableArray<int> _immutableArray;
        private IEnumerable<int> _enumerable;

        [GlobalSetup]
        public void Setup()
        {
            _immutableArray = [.. Enumerable.Range(0, 1000)];
            _enumerable = _immutableArray;
        }

        [Benchmark]
        public long ImmutableArray()
        {
            var c = 0;
            foreach (var i in _immutableArray)
            {
                c++;
            }

            return c;
        }

        [Benchmark]
        public long IEnumerable()
        {
            var c = 0;
            foreach (var i in _enumerable)
            {
                c++;
            }

            return c;
        }
    }
}