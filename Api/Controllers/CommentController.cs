using Api.Interfaces;
using Api.Mappers;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Api.Mappers;
using Api.DTOs.Comment;

namespace Api.Controllers
{
    [Route("Api/Comments")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {

            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            if (!ModelState.IsValid)//проверяет чтобы везде были заполнены поля required и все условия удовлетворяли
            {
                return BadRequest(ModelState);
            }

            var comments = await _commentRepository.GetAllAsync();
            var commentsDto = comments.Select(s => s.ToCommentDto());
            return Ok(commentsDto);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            if (!ModelState.IsValid)//проверяет чтобы везде были заполнены поля required и все условия удовлетворяли
            {
                return BadRequest(ModelState);
            }

            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }
        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)

        {
            if (!ModelState.IsValid)//проверяет чтобы везде были заполнены поля required и все условия удовлетворяли
            {
                return BadRequest(ModelState);
            }

            var getStock = await _stockRepository.StockExists(stockId);
            if (!getStock)
            {
                return BadRequest("Stock does not exist.");
            }

            var commentModel = commentDto.ToCreateCommentDto(stockId);
            await _commentRepository.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetCommentById), new { id = commentModel }, commentDto);
        }

        [HttpPut("{stockId:int}/{commentId:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int stockId, [FromRoute] int commentId, [FromBody] UpdateCommentDto commentDto )
        {
            if (!ModelState.IsValid)//проверяет чтобы везде были заполнены поля required и все условия удовлетворяли
            {
                return BadRequest(ModelState);
            }
            var commentModel= await _commentRepository.UpdateAsync(stockId, commentId, commentDto);
            if(commentModel== null)
            {
                return NotFound();
            }

            return Ok(commentModel.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)//проверяет чтобы везде были заполнены поля required и все условия удовлетворяли
            {
                return BadRequest(ModelState);
            }
            var commentModel=await _commentRepository.DeleteAsync(id);

            if(commentModel== null)
            {
                return NotFound();
            }

            return Ok(commentModel);
        }
    }
}
