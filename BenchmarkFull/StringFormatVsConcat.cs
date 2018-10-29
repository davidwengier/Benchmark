using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;

namespace BenchmarkFull
{
    [MemoryDiagnoser]
    [RPlotExporter]
    public class StringFormatVsConcat
    {
        private readonly string _assemblyName = new string('a', 20);
        private readonly string _version = new string('1', 6);

        [Benchmark]
        public string Concat()
        {
            return string.Concat(_assemblyName, "\\", _version);
        }

        [Benchmark]
        public string Concat_Char()
        {
            return string.Concat(_assemblyName, '\\', _version);
        }

        [Benchmark]
        public string Plus()
        {
            return _assemblyName + "\\" + _version;
        }

        [Benchmark]
        public string Plus_Char()
        {
            return _assemblyName + '\\' + _version;
        }

        [Benchmark]
        public string StringFormat()
        {
            return string.Format("{0}\\{1}", _assemblyName, _version);
        }

        [Benchmark]
        public string Interpolated()
        {
            return $"{_assemblyName}\\{_version}";
        }
    }
}
