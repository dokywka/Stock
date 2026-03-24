using StockApp.Core.Common;
using StockApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace StockApp.Core.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Result<StockTransaction>> AddTransactionAsync(StockTransaction transaction);
        Task<Result<List<StockTransaction>>> GetUserTransactionsAsync(string userId);
    }
}
