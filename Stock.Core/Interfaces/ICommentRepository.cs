using StockApp.StockApp.Core.Models;

namespace StockApp.StockApp.Core.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment> UpdateAsync(int stockId, int commentId, Comment updateDto);
        Task<Comment> DeleteAsync(int id);
    }
}
