using AiStockAdvisor.Models;
using StockApp.StockApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiStockAdvisor.Interfaces
{
    public class IYandexService
    {
        Task<AiRecommendation> GetRecommendationAsync(StockItem stock);
    }
}
