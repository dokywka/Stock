using StockApp.Core.Common;
using StockApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockApp.Core.Interfaces
{
    public interface IFinnhubService
    {
        Task<Result<decimal>> GetActualStockCostAsync(string ticker);
        Task<Result<FinhubSearchResult>> SearchForStockByTicker(string query);
        Task<Result<FinnhubCompanyProfile>> GetCompanyProfileAsync(string ticker);
    }
}
