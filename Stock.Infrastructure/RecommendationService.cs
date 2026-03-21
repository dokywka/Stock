using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StockApp.Core.Common;
using StockApp.Core.Interfaces;
using StockApp.Core.Models;
using StockApp.StockApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StockApp.Infrastructure
{
    public class RecommendationService:IRecommendationService
    {
        private readonly AppDbContext _appDbContext;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public RecommendationService(AppDbContext appDbContext, HttpClient httpClient,IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<Result<AiRecommendation>> GetRecommendationByStock(string ticker)
        {
            var stock=await _appDbContext.Stocks.FirstOrDefaultAsync(x=>x.Symbol==ticker);
            if (stock==null)
                return Result<AiRecommendation>.Failure("Не удалось найти акции с таким тикером.");

            var recommendation=await _appDbContext.AiRecommendations.Where(x=>x.Symbol.Equals(ticker)&&x.ExpiresAt>DateTime.Now).FirstOrDefaultAsync();

            if (recommendation == null)
            {
                string prompt = "Ты финансовый аналитик. " +
                    "Проанализируй акцию и ответь ТОЛЬКО в JSON формате без лишнего текста:\r\n{\"action\": \"Buy/Sell/Hold\", " +
                    "\"explanation\": \"краткое объяснение\", \"confidence\": 75}\r\n\r\n" +
                    "Данные акции:\r\n" +
                    $"Тикер: {stock.Symbol}\r\n" +
                    $"Цена: {stock.Purchase}\r\n" +
                    $"Индустрия: {stock.Industry}\r\n" +
                    $"Капитализация: {stock.MarketCap}";

                var requestBody = new ModelRecommendation
                {
                    Model = "stepfun/step-3.5-flash:free",
                    RecommendationsList = new List<MessageRecommendation> { new MessageRecommendation { UserRole = "user", Content = prompt } }
                };

                string json = JsonSerializer.Serialize(requestBody);

                var content = new StringContent(json, Encoding.UTF8, System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json"));
                var response = await _httpClient.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);

                if (!response.IsSuccessStatusCode)
                {
                    string errorBody = await response.Content.ReadAsStringAsync();
                    return Result<AiRecommendation>.Failure($"OpenRouter error: {errorBody}");
                }

                string gptResponse= await response.Content.ReadAsStringAsync();
                var quote = JsonSerializer.Deserialize<OpenRouterResponse>(gptResponse);

                string gptContent = quote.Choices[0].Message.Content;

                gptContent = gptContent.Trim();
                if (gptContent.StartsWith("```"))
                {
                    gptContent = gptContent.Replace("```json", "").Replace("```", "").Trim();
                }

                var options = new JsonSerializerOptions();
                options.Converters.Add(new JsonStringEnumConverter());
                var toRecommendation = JsonSerializer.Deserialize<AiRecommendation>(gptContent, options);

                toRecommendation.StockItemId = stock.Id;
                toRecommendation.Symbol=stock.Symbol;
                toRecommendation.CreatedAt = DateTime.Now;
                toRecommendation.ExpiresAt = DateTime.Now.AddHours(6);

                _appDbContext.AiRecommendations.Add(toRecommendation);
                await _appDbContext.SaveChangesAsync();

                return Result<AiRecommendation>.Success(toRecommendation);
            }
            return Result<AiRecommendation>.Success(recommendation);
        }
    }
}
