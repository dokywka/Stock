using StockApp.Core.Models;
using StockApp.StockApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockApp.Core.Interfaces
{
    public interface ITransactionsPortfolioRepository
    {
        Task<decimal?> BuyTransactionToPortfolioAsync(StockUser user, int stockId, int quantity);
        Task<List<Portfolio>> GetAllPortfolioTransactionsAsync(StockUser user);
        Task<decimal?> SellTransactionFromPortfolioAsync(StockUser user, int stockId, int amount);
        Task<Portfolio?> GetByStockIdAsync(int stockId, StockUser user);

        Task<decimal> GetPortfolioValueAsync(StockUser user);

    }
}
