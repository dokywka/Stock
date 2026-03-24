using StockApp.Core.Models;
using StockApp.StockApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using StockApp.Core.Common;

namespace StockApp.Core.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<Result<decimal>> BuyTransactionToPortfolioAsync(StockUser user, int stockId, int quantity);
        Task<Result<List<Portfolio>>> GetAllPortfolioTransactionsAsync(StockUser user);
        Task<Result<decimal>> SellTransactionFromPortfolioAsync(StockUser user, int stockId, int amount);
        Task<Result<Portfolio>> GetByStockIdAsync(int stockId, StockUser user);
        Task<Result<decimal>> GetPortfolioValueAsync(StockUser user);

    }
}
