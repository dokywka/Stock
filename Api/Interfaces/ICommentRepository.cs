using Api.DTOs.Comment;
using Api.Models;

namespace Api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment> UpdateAsync(int stockId, int commentId, UpdateCommentDto updateDto);
        Task<Comment> DeleteAsync(int id);
    }
}
