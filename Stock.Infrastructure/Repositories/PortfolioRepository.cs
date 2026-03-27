using Microsoft.EntityFrameworkCore;
using StockApp.Core.Common;
using StockApp.Core.Interfaces;
using StockApp.Core.Models;
using StockApp.StockApp.Core.Models;
using StockApp.StockApp.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace StockApp.Infrastructure.Repositories
{
    public class PortfolioRepository: IPortfolioRepository
    {
        private readonly AppDbContext _appDbContext;
        public PortfolioRepository(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }

        public async Task<Result<decimal>> BuyTransactionToPortfolioAsync(StockUser user, int stockId, int quantity)
        {
            StockItem chosenStock = await _appDbContext.Stocks.FirstOrDefaultAsync(x => x.Id == stockId);

            if (chosenStock == null) return Result<decimal>.Failure("Акция не найдена");

            decimal cost = chosenStock.Purchase * quantity;

            if (user.Balance < cost) return Result<decimal>.Failure("Недостаточно средств на балансе");
            {
                user.Balance -= cost;
                Portfolio transaction = new Portfolio()
                {
                    UserId = user.Id,
                    StockId = stockId,
                    Quantity = quantity,
                    PurchasePrice = chosenStock.Purchase,
                    PurchaseDate = DateTime.Now,
                };
                _appDbContext.Add(transaction);
                _appDbContext.Update(user);
                await _appDbContext.SaveChangesAsync();
            }

            return Result<decimal>.Success(cost);
        }
        public async Task<Result<List<Portfolio>>> GetAllPortfolioTransactionsAsync(StockUser user)
        {
            List<Portfolio> portfolios=await _appDbContext.Portfolios.Where(x => x.UserId == user.Id).Include(x => x.Stock).ToListAsync();
            return Result<List<Portfolio>>.Success(portfolios);
        }
        public async Task<Result<decimal>> SellTransactionFromPortfolioAsync(StockUser user, int stockId, int amount)
        {
            Portfolio portfolio=await _appDbContext.Portfolios.Where(x=>x.UserId==user.Id && x.StockId==stockId).FirstOrDefaultAsync();

            StockItem stock = await _appDbContext.Stocks.FirstOrDefaultAsync(x => x.Id == stockId);
            if (stock == null) return Result<decimal>.Failure("Акция не найдена");

            if (portfolio==null) return Result<decimal>.Failure("Позиция в портфеле не найдена");

            if (amount > portfolio.Quantity) 
            {
                return Result<decimal>.Failure($"Нельзя продать {amount} акций, у вас только {portfolio.Quantity}");
            }
            else if(amount==portfolio.Quantity)
            {
                _appDbContext.Remove(portfolio);
            }
            else{
                portfolio.Quantity -= amount;
                _appDbContext.Update(portfolio);
            }

            decimal currentPrice = stock.Purchase; // обновляется фоновым сервисом
            decimal totalReturn = currentPrice * amount; // сколько получаем за продажу
            decimal profitLoss = (currentPrice - portfolio.PurchasePrice) * amount; // прибыль/убыток

            user.Balance += totalReturn;

            _appDbContext.Update(user);
            await _appDbContext.SaveChangesAsync();

            return Result<decimal>.Success(profitLoss);


        }
        public async Task<Result<Portfolio>> GetByStockIdAsync(int stockId,StockUser user)
        {
            Portfolio? stocks = await _appDbContext.Portfolios.Where(x => x.UserId == user.Id && x.StockId == stockId).FirstOrDefaultAsync();
            if (stocks == null) return Result<Portfolio>.Failure("Позиция не найдена");
            return Result<Portfolio>.Success(stocks);
        }
        public async Task<Result<decimal>> GetPortfolioValueAsync(StockUser user)
        {
            decimal overCost=0;

            var stocks = await _appDbContext.Portfolios.Where(x => x.UserId == user.Id).ToListAsync();
            foreach (var stock in stocks)
            {
                overCost += stock.Quantity * stock.PurchasePrice;
            }
            return Result<decimal>.Success(overCost);
        }

    }
}
