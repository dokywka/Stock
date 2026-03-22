using Microsoft.Extensions.Caching.Distributed;
using StockApp.Core.Common;
using StockApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace StockApp.Infrastructure
{
    public class CacheService:ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task<Result<T>> GetFromCacheAsync<T>(string key)
        {
            string data = await _distributedCache.GetStringAsync(key);

            if (data == null) return Result<T>.Failure("Не удалось получить данные от сервиса.");

            T result = JsonSerializer.Deserialize<T>(data);
            return Result<T>.Success(result);
        }
        public async Task<Result<T>> SetAsync<T>(string key, T value, TimeSpan expiry)
        {
            var result = JsonSerializer.Serialize<T>(value);
            DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow=expiry
            };

            await _distributedCache.SetStringAsync(key, result, cacheOptions);
            return Result<T>.Success(value);

        }

    }
}
