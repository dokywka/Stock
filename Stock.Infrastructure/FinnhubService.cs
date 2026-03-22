using Azure;
using Azure.Core.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using StockApp.Core.Interfaces;
using StockApp.Core.Models;
using StockApp.StockApp.Core.Models;
using StockApp.StockApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Timers;
using StockApp.Core.Common;

namespace StockApp.Infrastructure
{
    public class FinnhubService: IFinnhubService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;

        public FinnhubService(HttpClient httpClient, IConfiguration configuration,ICacheService cacheService) 
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _cacheService = cacheService;
        }
        public async Task<Result<decimal>> GetActualStockCostAsync(string ticker)
        {
            string key = _configuration.GetValue<string>("Finnhub:ApiKey");
            string cacheKey = $"stock:price:{ticker}";

            var cached = await _cacheService.GetFromCacheAsync<decimal>(cacheKey);//Проверяем кэш
            if (cached.IsSuccess) return cached;

            var response=await _httpClient.GetAsync($"https://finnhub.io/api/v1/quote?symbol={ticker}&token={key}");
            if (!response.IsSuccessStatusCode)
                return Result<decimal>.Failure("Finnhub недоступен");

            var json = await response.Content.ReadAsStringAsync();
            var quote = JsonSerializer.Deserialize<FinnhubQuote>(json);

            if (quote == null) return Result<decimal>.Failure("Не удалось десириализовать json файл");

            await _cacheService.SetAsync(cacheKey, quote.CurrentPrice, TimeSpan.FromSeconds(30));//сохранение в динамическую память(сохраняем в Redis)
            return Result<decimal>.Success(quote.CurrentPrice);
        }
        public async Task<Result<FinhubSearchResult>> SearchForStockByTicker(string query)
        {
            string key = _configuration.GetValue<string>("Finnhub:ApiKey");

            var response = await _httpClient.GetAsync($"https://finnhub.io/api/v1/search?q={query}&token={key}");
            if (!response.IsSuccessStatusCode)
                return Result<FinhubSearchResult>.Failure("Finnhub недоступен");

            string json =await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<FinhubSearchResult>(json);

            if (item == null) return Result<FinhubSearchResult>.Failure("Не удалось десириализовать json файл");

            return Result<FinhubSearchResult>.Success(item);
        }
        public async Task<Result<FinnhubCompanyProfile>> GetCompanyProfileAsync(string ticker)
        {
            string key = _configuration.GetValue<string>("Finnhub:ApiKey");
            string profileKey = $"stock: profile: { ticker}";//определяем ключ по которому будем хранить кэш с профилем

            var cached = await _cacheService.GetFromCacheAsync<FinnhubCompanyProfile>(profileKey);//пытаемся проверить есть ли уже сохраненные данные
            if (cached.IsSuccess) return cached;//возвращаем их

            var response = await _httpClient.GetAsync($"https://finnhub.io/api/v1/stock/profile2?symbol={ticker}&token={key}");
            if (!response.IsSuccessStatusCode)
                return Result<FinnhubCompanyProfile>.Failure("Finnhub недоступен");

            string json = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<FinnhubCompanyProfile>(json);

            if (item == null) return Result<FinnhubCompanyProfile>.Failure("Не удалось десириализовать json файл");

            await _cacheService.SetAsync(profileKey, item, TimeSpan.FromHours(1));//если нет, то проходим по обычному и сохраняем новое значение под ключом

            return Result<FinnhubCompanyProfile>.Success(item);
        }
    }
}
