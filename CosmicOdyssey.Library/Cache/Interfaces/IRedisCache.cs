﻿namespace CosmicOdyssey.Library.Cache.Interfaces;

public interface IRedisCache
{
    Task<T> GetRecordAsync<T>(string recordId);
    Task RemoveRecordAsync(string recordId);
    Task SetRecordAsync<T>(string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? unusedExpireTime = null);
}