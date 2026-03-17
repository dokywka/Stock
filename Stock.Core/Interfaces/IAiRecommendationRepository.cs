using System;
using System.Collections.Generic;
using AiStockAdvisor.Models;

namespace StockApp.Core.Interfaces
{
    public interface IAiRecommendationRepository
    {
        Task<AiRecommendation?> GetCurrentAsync(string symbol, int maxAgeHours = 6);

        Task SaveAsync(AiRecommendation recommendation);
    }
}
