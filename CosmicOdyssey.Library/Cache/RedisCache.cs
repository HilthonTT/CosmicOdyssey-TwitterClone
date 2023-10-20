using CosmicOdyssey.Library.Cache.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CosmicOdyssey.Library.Cache;
public class RedisCache : IRedisCache
{
    private readonly IDistributedCache _redis;
    private readonly IMemoryCache _cache;
    private readonly ILogger<RedisCache> _logger;

    public RedisCache(
        IDistributedCache redis,
        IMemoryCache cache,
        ILogger<RedisCache> logger)
    {
        _redis = redis;
        _cache = cache;
        _logger = logger;
    }

    public async Task SetRecordAsync<T>(
        string recordId,
        T data,
        TimeSpan? absoluteExpireTime = null,
        TimeSpan? unusedExpireTime = null)
    {
        string jsonData = JsonSerializer.Serialize(data);

        try
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromMinutes(30),
                SlidingExpiration = unusedExpireTime,
            };


            await _redis.SetStringAsync(recordId, jsonData, options);
        }
        catch (Exception ex)
        {
            _logger.LogError("Redis cache failed: {message}", ex.Message);

            var options = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromMinutes(30),
                SlidingExpiration = unusedExpireTime
            };

            _cache.Set(recordId, jsonData, options);
        }
    }

    public async Task<T> GetRecordAsync<T>(string recordId)
    {
        try
        {
            string jsonData = await _redis.GetStringAsync(recordId);
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }
        catch (Exception ex)
        {
            _logger.LogError("Redis cache failed: {message}", ex.Message);

            string jsonData = _cache.Get<string>(recordId);
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(jsonData);
        }
    }

    public async Task RemoveRecordAsync(string recordId)
    {
        try
        {
            _cache.Remove(recordId);
            await _redis.RemoveAsync(recordId);
        }
        catch (Exception ex)
        {
            _logger.LogError("Redis cache failed: {message}", ex.Message);
        }
    }
}
