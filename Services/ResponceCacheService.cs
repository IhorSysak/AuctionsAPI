using AuctionsAPI.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace AuctionsAPI.Services
{
    public class ResponceCacheService : IResponceCacheService
    {
        private readonly IDistributedCache _distributedCache;

        public ResponceCacheService(IDistributedCache distributedCache) 
        {
            _distributedCache = distributedCache;
        }

        public async Task CacheResponceAsync(string cacheKey, object responce, TimeSpan timeToLive)
        {
            if (responce == null) 
            {
                return;
            }

            var serializedResponce = JsonConvert.SerializeObject(responce);
            
            await _distributedCache.SetStringAsync(cacheKey, serializedResponce, new DistributedCacheEntryOptions 
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }

        public async Task<string> GetCacheResponceAsync(string cacheKey)
        {
            var cachedResponce = await _distributedCache.GetStringAsync(cacheKey);

            return string.IsNullOrEmpty(cachedResponce) ? null : cachedResponce;
        }
    }
}
