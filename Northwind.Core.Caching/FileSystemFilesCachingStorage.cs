using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Options;
using Northwind.Core.Caching.Interfaces;
using Northwind.Core.Caching.Options;

namespace Northwind.Core.Caching
{
    public class FileSystemFilesCachingStorage : IFilesCachingStorage
    {
        private static readonly object _filesLock = new object();

        private readonly CachingOptions _cachingOptions;

        public FileSystemFilesCachingStorage(IOptions<CachingOptions> cachingOptions)
        {
            _cachingOptions = cachingOptions.Value;
        }

        public IEnumerable<byte> Get(string fileName)
        {
            var path = _cachingOptions.DirectoryPath + fileName;
            lock (_filesLock)
            {
                if (File.Exists(path))
                {
                    return File.ReadAllBytes(path);
                }
            }

            return null;
        }

        public void Add(string fileName, IEnumerable<byte> file)
        {
            var path = _cachingOptions.DirectoryPath + fileName;
            lock (_filesLock)
            {
                if (!File.Exists(path) && Directory.GetFiles(_cachingOptions.DirectoryPath).Length < _cachingOptions.MaxCount)
                {
                    File.WriteAllBytes(path, file.ToArray());
                }
            }
        }

        public void ClearCache()
        {
            lock (_filesLock)
            {
                if (Directory.Exists(_cachingOptions.DirectoryPath))
                {
                    var directoryInfo = new DirectoryInfo(_cachingOptions.DirectoryPath);
                    foreach (var fileInfo in directoryInfo.EnumerateFiles())
                    {
                        fileInfo.Delete();
                    }
                }
            }
        }
    }
}
