using BenchmarkDotNet.Running;
using System;

namespace BenchmarkCore
{
    public class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run();
        }
    }
}