using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;

namespace BenchmarkFull
{
    [MemoryDiagnoser]
    [RPlotExporter]
    public class LocalFunctions
    {
        [Benchmark]
        public int LocalFunction()
        {
            var x = 1;

            Add(3);

            x += 1;

            return x;

            void Add(int amount)
            {
                x += amount;
            }
        }


        [Benchmark]
        public int LocalFunctionNoCapture()
        {
            var x = 1;

            x = Add(x, 3);

            x += 1;

            return x;

            int Add(int input, int amount)
            {
                return input + amount;
            }
        }

        [Benchmark]
        public int StaticLocalFunction()
        {
            var x = 1;

            x = Add(x, 3);

            x += 1;

            return x;

            static int Add(int input, int amount)
            {
                return input + amount;
            }
        }


        [Benchmark(Baseline = true)]
        public int StaticMethod()
        {
            var x = 1;

            x = StaticAdd(x, 3);

            x += 1;

            return x;
        }

        private static int StaticAdd(int input, int amount)
        {
            return input + amount;
        }

        [Benchmark]
        public int InstanceMethod()
        {
            var x = 1;

            x = InstanceAdd(x, 3);

            x += 1;

            return x;
        }

        private static int InstanceAdd(int input, int amount)
        {
            return input + amount;
        }

        [Benchmark]
        public int LambdaNoCapture()
        {
            Func<int, int, int> adder = (input, amount) =>
            {
                return input + amount;
            };

            var x = 1;

            x = adder(x, 3);

            x += 1;

            return x;
        }

        [Benchmark]
        public int Lambda()
        {
            var x = 1;

            Action<int> adder = (amount) =>
            {
                x += amount;
            };

            adder(3);

            x += 1;

            return x;
        }
    }
}
