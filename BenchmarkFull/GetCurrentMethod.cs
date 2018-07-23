using BenchmarkDotNet.Attributes;
using System.Diagnostics;
using System.Reflection;

namespace BenchmarkFull
{
    [MemoryDiagnoser]
    public class GetCurrentMethod
    {
        [Benchmark]
        public MethodBase MethodBaseCurrentMethod()
        {
            return MethodBase.GetCurrentMethod();
        }

        [Benchmark]
        public MethodBase NewStackFrameGetMethod()
        {
            return new StackFrame(1).GetMethod();
        }
    }
}