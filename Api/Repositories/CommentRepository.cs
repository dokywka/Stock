using Api.DTOs.Comment;
using Api.Interfaces;
using Api.Mappers;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context) 
        { 

        _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment> DeleteAsync(int id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (commentModel == null)
            {
                return null;
            }

            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            var comments = await _context.Comments.ToListAsync();

            return comments;
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            return comment;
        }

        public async Task<Comment> UpdateAsync(int stockId, int commentId, UpdateCommentDto updateDto)
        {
            var stock=await _context.Stocks.FirstOrDefaultAsync(x=>x.Id== stockId);
            if ( stock == null)
            {
                return null;
            }
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == commentId && x.StockId == stockId);
            if (comment == null)
            {
                return null;
            }

            comment.ToUpdateCommentDto(updateDto); // <--- обновляем существующий объект

            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
