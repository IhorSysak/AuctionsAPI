namespace AuctionsAPI.Interfaces
{
    public interface IResponceCacheService
    {
        Task CacheResponceAsync(string cacheKey, object responce, TimeSpan timeToLive);
        Task<string> GetCacheResponceAsync(string cacheKey);
    }
}
