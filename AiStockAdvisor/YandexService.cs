using AiStockAdvisor.Interfaces;
using AiStockAdvisor.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StockApp.StockApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiStockAdvisor
{
    public class YandexService: IYandexService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly ILogger<YandexService> _logger;
        public YandexService(IConfiguration configuration, HttpClient httpClient,ILogger<YandexService> logger)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _logger=logger;
        }
        public Task<AiRecommendation> GetRecommendationAsync(StockItem stock)
        {

        }
    }
}
