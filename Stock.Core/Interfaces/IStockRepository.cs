using StockApp.StockApp.Core.Queries;
using StockApp.StockApp.Core.Models;

namespace StockApp.StockApp.Core.Interfaces
{
    public interface IStockRepository
    {
        Task<List<StockItem>> GetAllAsync(QueryObject query);
        Task<StockItem?> GetByIdAsync(int id);
        Task<StockItem?> CreateAsync(StockItem stockModel);
        Task<StockItem?> UpdateAsync(int id, StockItem stockModel);
        Task<StockItem?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}
