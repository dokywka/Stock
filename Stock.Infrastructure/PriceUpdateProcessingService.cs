using Microsoft.EntityFrameworkCore;
using StockApp.Core.Interfaces;
using StockApp.StockApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockApp.StockApp.Infrastructure
{
    public class PriceUpdateProcessingService:IPriceUpdateProcessingService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IFinnhubService _finnhubServce;
        public PriceUpdateProcessingService(AppDbContext appDbContext, IFinnhubService finnhubService) 
        {
            _appDbContext = appDbContext;
            _finnhubServce = finnhubService;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            List<StockItem> stocks = await _appDbContext.Stocks.ToListAsync(cancellationToken);

            foreach (StockItem stock in stocks)
            {
                var getPrice =await _finnhubServce.GetActualStockCostAsync(stock.Symbol);
                if (getPrice.IsSuccess)
                {
                    stock.Purchase = getPrice.Data;
                }

            }

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
