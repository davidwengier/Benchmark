using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkFull
{
    [MemoryDiagnoser]
    [RPlotExporter]
    public class EmptyHashsetContains
    {
        private List<ImmutableHashSet<string>> _sets;

        [GlobalSetup]
        public void Setup()
        {
            _sets = new List<ImmutableHashSet<string>>();
            foreach (var item in GetInputData())
            {
                _sets.Add(item[0] as ImmutableHashSet<string>);
            }
        }

        public static IEnumerable<object[]> GetInputData()
        {
            yield return new object[] { ImmutableHashSet<string>.Empty };
            yield return new object[] { ImmutableHashSet.CreateBuilder<string>().ToImmutable() };
            yield return new object[] { ImmutableHashSet.CreateRange<string>(new string[] { "fish" }) };
            yield return new object[] { ImmutableHashSet.CreateRange<string>(new string[] { "fish", "pants" }) };
            yield return new object[] { ImmutableHashSet.CreateRange<string>(new string[] { "fish", "pants", "are", "always", "wet" }) };

            var builder = ImmutableHashSet.CreateBuilder<string>();
            builder.Add("fish");
            builder.Remove("fish");
            yield return new object[] { builder.ToImmutable() };

            builder = ImmutableHashSet.CreateBuilder<string>();
            builder.Add("fish");
            var col = builder.ToImmutable();
            builder = col.ToBuilder();
            builder.Remove("fish");
            yield return new object[] { builder.ToImmutable() };
        }

        [Benchmark(Baseline = true)]
        public bool Empty_Contains() { var set = _sets[0]; return set.Contains("dinosaur"); }
        [Benchmark]
        public bool Empty_EmptyCheck() { var set = _sets[0]; return set.Count > 0 && set.Contains("dinosaur"); }


        [Benchmark]
        public bool EmptyFromBuilder_Contains() { var set = _sets[1]; return set.Contains("dinosaur"); }
        [Benchmark]
        public bool EmptyFromBuilder_EmptyCheck() { var set = _sets[1]; return set.Count > 0 && set.Contains("dinosaur"); }

        [Benchmark]
        public bool OneItem_Contains() { var set = _sets[2]; return set.Contains("dinosaur"); }
        [Benchmark]
        public bool OneItem_EmptyCheck() { var set = _sets[2]; return set.Count > 0 && set.Contains("dinosaur"); }

        [Benchmark]
        public bool TwoItems_Contains() { var set = _sets[3]; return set.Contains("dinosaur"); }
        [Benchmark]
        public bool TwoItems_EmptyCheck() { var set = _sets[3]; return set.Count > 0 && set.Contains("dinosaur"); }

        [Benchmark]
        public bool FiveItems_Contains() { var set = _sets[4]; return set.Contains("dinosaur"); }
        [Benchmark]
        public bool FiveItems_EmptyCheck() { var set = _sets[4]; return set.Count > 0 && set.Contains("dinosaur"); }

        [Benchmark]
        public bool OneItemAddedAndRemoved_Contains() { var set = _sets[5]; return set.Contains("dinosaur"); }
        [Benchmark]
        public bool OneItemAddedAndRemoved_EmptyCheck() { var set = _sets[5]; return set.Count > 0 && set.Contains("dinosaur"); }

        [Benchmark]
        public bool OneItemAddedAndRemovedViaBuilders_Contains() { var set = _sets[6]; return set.Contains("dinosaur"); }
        [Benchmark]
        public bool OneItemAddedAndRemovedViaBuilders_EmptyCheck() { var set = _sets[6]; return set.Count > 0 && set.Contains("dinosaur"); }
    }
}
