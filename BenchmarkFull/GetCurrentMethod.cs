using System.Diagnostics;
using System.Reflection;
using BenchmarkDotNet.Attributes;

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