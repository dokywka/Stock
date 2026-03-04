using StockApp.StockApp.Core.Interfaces;
using StockApp.StockApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using StockApp.StockApp.Infrastructure;


namespace StockApp.StockApp.Infrastructure.Repositories;

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

    public async Task<Comment> UpdateAsync(int stockId, int commentId, Comment updateComment)
    {
        var comment = await _context.Comments
            .FirstOrDefaultAsync(x => x.Id == commentId && x.StockId == stockId);
        if (comment == null)
            return null;
        comment.Title = updateComment.Title;
        comment.Content = updateComment.Content;
        await _context.SaveChangesAsync();
        return comment;
    }
}
