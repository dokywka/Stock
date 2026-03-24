using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockApp.Api.DTOs.Stock;
using StockApp.Core.Common;
using StockApp.Core.Interfaces;
using StockApp.Core.Models;
using StockApp.Core.Queries;
using StockApp.Infrastructure;
using StockApp.Infrastructure.Handlers;
using StockApp.StockApp.Api.DTOs.Stock;
using StockApp.StockApp.Api.Mappers;
using StockApp.StockApp.Api.Mappers;
using StockApp.StockApp.Core.Interfaces;
using StockApp.StockApp.Core.Models;
using StockApp.StockApp.Core.Queries;
using System.Timers;

namespace StockApp.StockApp.Api.Controllers
{
    [Route("Api/Stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;
        private readonly IFinnhubService _finnhubService;
        private readonly IMediator _mediator;
        public StockController(IStockRepository stockRepo, IFinnhubService finnhubService, IMediator mediator)
        {
            _stockRepo = stockRepo;
            _mediator=mediator;
            _finnhubService = finnhubService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject  query)//fromquery возможность добавлять query параметры .../stocks=Tesla для фильтрации или поиска 
        {
            if (!ModelState.IsValid)//проверяет чтобы везде были заполнены поля required и все условия удовлетворяли
            {
                return BadRequest(ModelState);
            }

            var result = await _mediator.Send(new GetAllStocksQuery{ Query=query});

            if (!result.IsSuccess) return BadRequest(result.Error);
            var stocksDto = result.Data.Select(s => s.ToStockDto());

            return Ok(stocksDto);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            if (!ModelState.IsValid)//проверяет чтобы везде были заполнены поля required и все условия удовлетворяли
            {
                return BadRequest(ModelState);
            }

            var stock = await _stockRepo.GetByIdAsync(id);
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
            await _stockRepo.CreateAsync(stockModel);
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
            var stockModel = await _stockRepo.UpdateAsync(id, updateDto.ToStockFromUpdateDto());
            if (stockModel == null)
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
            var stockModel= await _stockRepo.DeleteAsync(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpGet]
        [Route("price/{ticker}")]
        public async Task<IActionResult> GetCurrentStockCostAsync(string ticker)
        {
            Result<decimal> cost =await _finnhubService.GetActualStockCostAsync(ticker);
            if (!cost.IsSuccess) 
                return BadRequest(cost.Error);

            return Ok(cost.Data);
        }
        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchStockBySymbolOrDescription([FromQuery]string query)
        {
            Result<FinhubSearchResult> content=await _finnhubService.SearchForStockByTicker(query);

            if(!content.IsSuccess)
                return BadRequest(content.Error);

            return Ok(content.Data);
        }
        [HttpPost]
        [Route("add/{ticker}")]
        public async Task<IActionResult> AddStockByTicker([FromRoute] string ticker)
        {
            Result<FinnhubCompanyProfile> profile = await _finnhubService.GetCompanyProfileAsync(ticker);
            if (!profile.IsSuccess) return BadRequest(profile.Error);

            Result<decimal> price = await _finnhubService.GetActualStockCostAsync(ticker);
            if (!price.IsSuccess) return BadRequest(price.Error);

            StockItem stock = new StockItem
            {
                Symbol = ticker,
                CompanyName = profile.Data.Name,
                Purchase = price.Data,
                Industry = profile.Data.FinnhubIndustry,
                MarketCap = (long)profile.Data.MarketCapitalization
            };

            await _stockRepo.CreateAsync(stock);
            return Ok(stock.ToStockDto());
        }
    }
}
