using Api.DTOs.Comment;
using Api.Models;

namespace Api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId
            };
        }
        public static Comment ToCreateCommentDto(this CreateCommentDto commentDto,int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId
            };
        }
        public static void ToUpdateCommentDto(this Comment comment, UpdateCommentDto commentDto)//чтобы не создавало новый объект, а изменяло уже существующий без new
        {
            comment.Title = commentDto.Title;
            comment.Content = commentDto.Content;
            comment.CreatedOn = commentDto.CreatedOn;
        }
    }
}
