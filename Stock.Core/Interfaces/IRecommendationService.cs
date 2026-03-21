using StockApp.Core.Common;
using StockApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockApp.Core.Interfaces
{
    public interface IRecommendationService
    {
        Task<Result<AiRecommendation>> GetRecommendationByStock(string ticker);
    }
}
