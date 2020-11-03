using System.Collections.Generic;

namespace Northwind.Core.Caching.Interfaces
{
    public interface IFilesCachingService
    {
        IEnumerable<byte> Get(string key);

        void Add(string key, IEnumerable<byte> file);
    }
}
