using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Exporters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkFull
{
    [MemoryDiagnoser]
    [RPlotExporter]
    public class FileVsFileSystemInfo
    {
        private readonly string _fileName = typeof(FileVsFileSystemInfo).Assembly.Location;
        private readonly string _nonExistentFile = @"C:\blah\blah.txt";

        [Benchmark]
        public DateTime FileSystemInfo_Exists()
        {
            return new FileInfo(_fileName).LastWriteTimeUtc;
        }

        [Benchmark]
        public DateTime FileGetLastWriteTimeUtc_Exists()
        {
            return File.GetLastWriteTimeUtc(_fileName);
        }

        [Benchmark]
        public DateTime FileSystemInfo_NotExists()
        {
            return new FileInfo(_nonExistentFile).LastWriteTimeUtc;
        }

        [Benchmark]
        public DateTime FileGetLastWriteTimeUtc_NotExists()
        {
            return File.GetLastWriteTimeUtc(_nonExistentFile);
        }

        [Benchmark]
        public DateTime FileSystemInfo_Exists_WithCheck()
        {
            var file = new FileInfo(_fileName);
            return file.Exists ? file.LastWriteTimeUtc : DateTime.MinValue;
        }

        [Benchmark]
        public DateTime FileGetLastWriteTimeUtc_Exists_WithCheck()
        {
            return File.Exists(_fileName) ? File.GetLastWriteTimeUtc(_fileName) : DateTime.MinValue;
        }

        [Benchmark]
        public DateTime FileSystemInfo_NotExists_WithCheck()
        {
            var file = new FileInfo(_nonExistentFile);
            return file.Exists ? file.LastWriteTimeUtc : DateTime.MinValue;
        }

        [Benchmark]
        public DateTime FileGetLastWriteTimeUtc_NotExists_WithCheck()
        {
            return File.Exists(_fileName) ? File.GetLastWriteTimeUtc(_nonExistentFile) : DateTime.MinValue;
        }
    }
}
