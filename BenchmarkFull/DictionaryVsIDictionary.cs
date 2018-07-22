using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;

namespace BenchmarkFull
{
	[MemoryDiagnoser]
	[RPlotExporter]
	public class DictionaryVsIDictionary
	{
		private Dictionary<string, string> dict;
		private IDictionary<string, string> idict;

		[GlobalSetup]
		public void GlobalSetup()
		{
			dict = new Dictionary<string, string>();
			idict = (IDictionary<string, string>)dict;
		}

		[Benchmark]
		public Dictionary<string, string> DictionaryEnumeration()
		{
			foreach (var item in dict)
			{
			}
			return dict;
		}

		[Benchmark]
		public IDictionary<string, string> IDictionaryEnumeration()
		{
			foreach (var item in idict)
			{
			}
			return idict;
		}
	}
}