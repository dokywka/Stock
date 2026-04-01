using StockApp.Core.Common;
using StockApp.Core.Interfaces;
using StockApp.Core.Models;
using StockApp.Infrastructure.Repositories;
using StockApp.StockApp.Core.Interfaces;
using StockApp.StockApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ZstdSharp.Unsafe;

namespace StockApp.Infrastructure
{
    public class TradingService:ITradingService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IStockRepository _stockRepository;
        public TradingService(IPortfolioRepository portfolioRepository, ITransactionRepository transactionRepository,IStockRepository stockRepository) 
        {
            _portfolioRepository=portfolioRepository;
            _transactionRepository=transactionRepository;
            _stockRepository=stockRepository;
        }
        public async Task<Result<decimal>> SellStockAsync(StockUser user,int stockId,int quantity)
        {
            var result = await _portfolioRepository
                .SellTransactionFromPortfolioAsync(user, stockId, quantity);

            if (!result.IsSuccess)
                return Result<decimal>.Failure(result.Error);

            var stock = await _stockRepository.GetByIdAsync(stockId);

            await AddTransactionToProfileAsync(new StockTransaction
            {
                UserId = user.Id,
                StockSymbol = stock.Symbol,
                Quantity = quantity,
                Price = result.Data,
                Type = TransactionType.Sell,
                Date = DateTime.Now
            });

            return Result<decimal>.Success(result.Data);
        }
        public async Task<Result<decimal>> BuyStockAsync(StockUser user, int stockId, int quantity)
        {
            var result=await _portfolioRepository.BuyTransactionToPortfolioAsync(user, stockId, quantity);
            if (!result.IsSuccess)
                return Result<decimal>.Failure(result.Error);

            var stock = await _stockRepository.GetByIdAsync(stockId);

            await AddTransactionToProfileAsync(new StockTransaction
            {
                UserId = user.Id,
                StockSymbol = stock.Symbol,
                Quantity = quantity,
                Price = result.Data,
                Type = TransactionType.Buy,
                Date = DateTime.Now
            });


            return Result<decimal>.Success(result.Data);
        }

        public async Task<Result<List<StockTransaction>>> GetTransactionsAsync(string userId)
        {
            var result=await _transactionRepository.GetUserTransactionsAsync(userId);
            if (!result.IsSuccess)
                return Result<List<StockTransaction>>.Failure(result.Error);

            return Result<List<StockTransaction>>.Success(result.Data);
        }
        public async Task<Result<StockTransaction>> AddTransactionToProfileAsync(StockTransaction transaction)
        {
            var result = await _transactionRepository.AddTransactionAsync(transaction);

            if (!result.IsSuccess)
                return Result<StockTransaction>.Failure(result.Error);

            return Result<StockTransaction>.Success(result.Data);
        }

        public async Task<Result<decimal>> GetPortfolioValueAsync(StockUser user)
        {
            var result=await _portfolioRepository.GetPortfolioValueAsync(user);

            if (!result.IsSuccess)
                return Result<decimal>.Failure(result.Error);

            return Result<decimal>.Success(result.Data);

        }
        public async Task<Result<List<Portfolio>>> GetProfileBoughtStocks(StockUser user)
        {
            var result = await _portfolioRepository.GetProfileBoughtStocks(user);
            if (!result.IsSuccess)
                return Result<List<Portfolio>>.Failure(result.Error);

            return Result<List<Portfolio>>.Success(result.Data);
        }

    }
}
