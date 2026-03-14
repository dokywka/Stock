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

        public FinnhubService(HttpClient httpClient, IConfiguration configuration) 
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<Result<decimal>> GetActualStockCostAsync(string ticker)
        {
            string key = _configuration.GetValue<string>("Finnhub:ApiKey");

            var response=await _httpClient.GetAsync($"https://finnhub.io/api/v1/quote?symbol={ticker}&token={key}");
            if (!response.IsSuccessStatusCode)
                return Result<decimal>.Failure("Finnhub недоступен");

            var json = await response.Content.ReadAsStringAsync();
            var quote = JsonSerializer.Deserialize<FinnhubQuote>(json);

            if (quote == null) return Result<decimal>.Failure("Не удалось десириализовать json файл");

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

            var response = await _httpClient.GetAsync($"https://finnhub.io/api/v1/stock/profile2?symbol={ticker}&token={key}");
            if (!response.IsSuccessStatusCode)
                return Result<FinnhubCompanyProfile>.Failure("Finnhub недоступен");

            string json = await response.Content.ReadAsStringAsync();
            var item = JsonSerializer.Deserialize<FinnhubCompanyProfile>(json);

            if (item == null) return Result<FinnhubCompanyProfile>.Failure("Не удалось десириализовать json файл");

            return Result<FinnhubCompanyProfile>.Success(item);
        }
    }
}
