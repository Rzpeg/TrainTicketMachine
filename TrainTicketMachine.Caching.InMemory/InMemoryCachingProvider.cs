using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using TrainTicketMachine.Common.Caching;

namespace TrainTicketMachine.Caching.InMemory
{
    public class InMemoryCachingProvider : ICacheProvider
    {
        private readonly ConcurrentDictionary<string, object> cache = new ConcurrentDictionary<string, object>();

        public async Task<TResult> GetOrSet<TResult>(string cacheKey, Func<Task<TResult>> cacheSetter)
        {
            if (this.cache.TryGetValue(cacheKey, out var cachedValue))
            {
                return (TResult)cachedValue;
            }
            else
            {
                var valueToCache = await cacheSetter()
                    .ConfigureAwait(false);

                this.cache.AddOrUpdate(cacheKey, valueToCache, (key, oldValue) => valueToCache);

                return valueToCache;
            }
        }
    }
}
