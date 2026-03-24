using MediatR;
using StockApp.Core.Common;
using StockApp.Core.Queries;
using StockApp.StockApp.Core.Interfaces;
using StockApp.StockApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockApp.Infrastructure.Handlers
{
    
    public class GetAllStocksHandler:IRequestHandler<GetAllStocksQuery, Result<List<StockItem>>>
    {
        private readonly IStockRepository _stockRepository;
        public GetAllStocksHandler(IStockRepository stockRepository)
        {
            _stockRepository=stockRepository;
        }
        public async Task<Result<List<StockItem>>> Handle(GetAllStocksQuery request, CancellationToken cancellationToken)
        {
            var stocks = await _stockRepository.GetAllAsync(request.Query);
            return Result<List<StockItem>>.Success(stocks);
        }
    }
}
