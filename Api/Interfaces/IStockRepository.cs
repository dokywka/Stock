using Api.DTOs.Stock;
using Api.Helpers;
using Api.Models;

namespace Api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock?> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockModel);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}
