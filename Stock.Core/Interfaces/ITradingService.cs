using StockApp.Core.Common;
using StockApp.Core.Models;
using StockApp.StockApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockApp.Core.Interfaces
{
    public interface ITradingService
    {
        Task<Result<decimal>> BuyStockAsync(StockUser user, int stockId, int quantity);
        Task<Result<decimal>> SellStockAsync(StockUser user, int stockId, int quantity);
        Task<Result<List<StockTransaction>>> GetTransactionsAsync(string userId);
        Task<Result<StockTransaction>> AddTransactionToProfileAsync(StockTransaction transaction);
        Task<Result<decimal>> GetPortfolioValueAsync(StockUser user);
        Task<Result<List<Portfolio>>> GetProfileBoughtStocks(StockUser user);
    }
}
