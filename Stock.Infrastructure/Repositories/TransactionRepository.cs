using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using StockApp.Core.Common;
using StockApp.Core.Models;
using StockApp.Core.Interfaces;
using StockApp.StockApp.Infrastructure;

namespace StockApp.Infrastructure.Repositories
{
    public class TransactionRepository: ITransactionRepository
    {
        private readonly AppDbContext _appDbContext;
        public TransactionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Result<StockTransaction>> AddTransactionAsync(StockTransaction transaction)
        {
            _appDbContext.Transactions.Add(transaction);
            await _appDbContext.SaveChangesAsync();

            return Result<StockTransaction>.Success(transaction);
        }
        public async Task<Result<List<StockTransaction>>> GetUserTransactionsAsync(string userId)
        {
            var transactions = await _appDbContext.Transactions
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Date)
                .ToListAsync();

            if (!transactions.Any())
                return Result<List<StockTransaction>>.Failure("Транзакции не найдены");

            return Result<List<StockTransaction>>.Success(transactions);
        }
    }
}
