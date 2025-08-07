using Api.Mappers;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Api.DTOs;
using Api.DTOs.Stock;
using Microsoft.EntityFrameworkCore;
using Api.Interfaces;
using Api.Helpers;

namespace Api.Controllers
{
    [Route("Api/Stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IStockRepository stockRepo;
        public StockController(AppDbContext context,IStockRepository _stockRepo)
        {
            stockRepo = _stockRepo;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject  query)//fromquery возможность добавлять query параметры .../stocks=Tesla для фильтрации или поиска 
        {
            if (!ModelState.IsValid)//проверяет чтобы везде были заполнены поля required и все условия удовлетворяли
            {
                return BadRequest(ModelState);
            }

            var stocks = await stockRepo.GetAllAsync(query);
             
            var stocksDto=stocks.Select(s => s.ToStockDto());

            return Ok(stocksDto);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            if (!ModelState.IsValid)//проверяет чтобы везде были заполнены поля required и все условия удовлетворяли
            {
                return BadRequest(ModelState);
            }

            var stock = await stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(stock.ToStockDto());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)//проверяет чтобы везде были заполнены поля required и все условия удовлетворяли
            {
                return BadRequest(ModelState);
            }

            var stockModel = stockDto.ToStockFromCreateDto();
            await stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)//FromRoute это берем наш айди и по нему ищем , а FromBody это мы можем менять тело нашего json 
        {
            if (!ModelState.IsValid)//проверяет чтобы везде были заполнены поля required и все условия удовлетворяли
            {
                return BadRequest(ModelState);
            }
            var stockModel = await stockRepo.UpdateAsync(id, updateDto);
            if(stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)//проверяет чтобы везде были заполнены поля required и все условия удовлетворяли
            {
                return BadRequest(ModelState);
            }
            var stockModel= await stockRepo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
