using MediatR;
using StockApp.Core.Common;
using StockApp.StockApp.Core.Models;
using StockApp.StockApp.Core.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockApp.Core.Queries
{
    public class GetAllStocksQuery:IRequest<Result<List<StockItem>>>
    {
        public QueryObject Query { get; set; }
    }
}
