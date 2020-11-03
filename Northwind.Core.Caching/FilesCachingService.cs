using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Northwind.Core.Caching.Interfaces;
using Northwind.Core.Caching.Options;

namespace Northwind.Core.Caching
{
    public class FilesCachingService :IFilesCachingService, IAsyncDisposable
    {
        private readonly IFilesCachingStorage _cache;
        private readonly CachingOptions _cachingOptions;
        private readonly Timer _cachingTimer;

        public FilesCachingService(IFilesCachingStorage cache, IOptions<CachingOptions> cachingOptions)
        {
            _cache = cache;
            _cachingOptions = cachingOptions.Value;
            _cachingTimer = new Timer(_ => OnTimerIsOver(), null, _cachingOptions.ExpirationTimeMs, Timeout.Infinite);
        }

        public IEnumerable<byte> Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            RescheduleTimer();

            return _cache.Get(key);
        }

        public void Add(string key, IEnumerable<byte> file)
        {
            if (!string.IsNullOrWhiteSpace(key) && file != null)
            {
                RescheduleTimer();

                _cache.Add(key, file);
            }
        }

        public ValueTask DisposeAsync()
        {
            return ((IAsyncDisposable)_cachingTimer).DisposeAsync();
        }

        private void RescheduleTimer()
        {
            _cachingTimer.Change(_cachingOptions.ExpirationTimeMs, Timeout.Infinite);
        }

        private void OnTimerIsOver() => _cache.ClearCache();
    }
}
