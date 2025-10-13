﻿namespace netflix_clone_media.Api.Infrastructure.ResponseCache;

public interface IResponseCacheService
{
    Task SetAsync(string key, string value, TimeSpan? expiry = null);
    Task<string?> GetAsync(string key);
    Task RemoveAsync(string key);
}