using StockApp.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockApp.Core.Interfaces
{
    public interface ICacheService
    {
        Task<Result<T>> GetFromCacheAsync<T>(string key);
        Task<Result<T>> SetAsync<T>(string key,T value,TimeSpan expiry);
    }
}
