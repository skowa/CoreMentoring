using System.Collections.Generic;

namespace Northwind.Core.Caching.Interfaces
{
    public interface IFilesCachingStorage
    {
        IEnumerable<byte> Get(string fileName);

        void Add(string fileName, IEnumerable<byte> file);

        void ClearCache();
    }
}
