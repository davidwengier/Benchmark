using System;
using System.Linq;
using BenchmarkDotNet.Running;

namespace BenchmarkFull
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run();
        }
    }
}