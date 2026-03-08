using StockApp.Core.Models;
using StockApp.StockApp.Core.Models;
using StockApp.StockApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using StockApp.Core.Interfaces;

namespace StockApp.Infrastructure.Repositories
{
    public class TransactionPortfolioRepository: ITransactionsPortfolioRepository
    {
        private readonly AppDbContext _appDbContext;
        public TransactionPortfolioRepository(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }

        public async Task<decimal?> BuyTransactionToPortfolioAsync(StockUser user, int stockId, int quantity)
        {
            StockItem chosenStock = await _appDbContext.Stocks.FirstOrDefaultAsync(x => x.Id == stockId);

            if (chosenStock == null) return null;

            decimal cost = chosenStock.Purchase * quantity;

            if (user.Balance < cost) return null;
            {
                user.Balance -= cost;
                Portfolio transaction = new Portfolio()
                {
                    UserId = user.Id,
                    StockId = stockId,
                    Quantity = quantity,
                    PurchasePrice = cost,
                    PurchaseDate = DateTime.Now,
                };
                _appDbContext.Add(transaction);
                _appDbContext.Update(user);
                await _appDbContext.SaveChangesAsync();
            }

            return cost;
        }
        public async Task<List<Portfolio>> GetAllPortfolioTransactionsAsync(StockUser user)
        {
            List<Portfolio> portfolios=await _appDbContext.Portfolios.Where(x => x.UserId == user.Id).Include(x => x.Stock).ToListAsync();
            return portfolios;
        }
        public async Task<decimal?> SellTransactionFromPortfolioAsync(StockUser user, int stockId, int amount)
        {
            Portfolio portfolio=await _appDbContext.Portfolios.Where(x=>x.UserId==user.Id && x.StockId==stockId).FirstOrDefaultAsync();


            if (portfolio==null) return null;

            if (amount > portfolio.Quantity) 
            {
                return null;
            }
            else if(amount==portfolio.Quantity)
            {
                _appDbContext.Remove(portfolio);
            }
            else{
                portfolio.Quantity -= amount;
                _appDbContext.Update(portfolio);
            }

            decimal profit = portfolio.PurchasePrice * amount;
            user.Balance += profit;

            _appDbContext.Update(user);
            await _appDbContext.SaveChangesAsync();

            return profit;
            

        }
        public async Task<Portfolio?> GetByStockIdAsync(int stockId,StockUser user)
        {
            Portfolio? stocks = await _appDbContext.Portfolios.Where(x => x.UserId == user.Id && x.StockId == stockId).FirstOrDefaultAsync();
            return stocks;
        }
        public async Task<decimal> GetPortfolioValueAsync(StockUser user)
        {
            decimal overCost=0;

            var stocks = await _appDbContext.Portfolios.Where(x => x.UserId == user.Id).ToListAsync();
            foreach (var stock in stocks)
            {
                overCost += stock.Quantity * stock.PurchasePrice;
            }
            return overCost;
        }

    }
}
