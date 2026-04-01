using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockApp.Api.Controllers;
using StockApp.Api.DTOs.Portfolio;
using StockApp.Api.DTOs.Stock;
using StockApp.Api.DTOs.Transactions;
using StockApp.Core.Common;
using StockApp.Core.Interfaces;
using StockApp.StockApp.Core.Models;

namespace StockApp.StockApp.Api.Controllers
{
    [Route("Api/Portfolio")]
    [ApiController]
    public class PortfolioController:BaseController
    {
        private readonly ITradingService _tradingService;
        public PortfolioController(UserManager<StockUser> userManager, ITradingService tradingService) : base(userManager)
        {
            _tradingService = tradingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPortfolioAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return Unauthorized();

            var allStocks = await _tradingService.GetTransactionsAsync(user.Id);
            return Ok(allStocks);

        }
        [HttpGet("value")]
        public async Task<IActionResult> GetPorfolioValueAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return Unauthorized();

            var portfolioCost=await _tradingService.GetPortfolioValueAsync(user);
            return Ok(portfolioCost);
        }
        [HttpPost("buy")]
        public async Task<IActionResult> BuyStockAsync([FromBody]BuyStockDto model)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return Unauthorized();

            int stockId=model.StockId;
            int quantity=model.Quantity;



            Result<decimal> tryBuyTransaction=await _tradingService.BuyStockAsync(user,stockId,quantity);
            if (!tryBuyTransaction.IsSuccess) return BadRequest(tryBuyTransaction.Error);

            return Ok("Акция успешно куплена");
        }
        [HttpDelete]
        public async Task<IActionResult> SellStockAsync([FromBody]SellStockDto model)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return Unauthorized();

            int stockId=model.StockId;
            int amount = model.Quantity;

            Result<decimal> trySellTransaction = await _tradingService.SellStockAsync(user, stockId, amount);

            if (!trySellTransaction.IsSuccess) return BadRequest(trySellTransaction.Error);

            return Ok($"Успешная продажа, выгода {trySellTransaction.Data}");
        }

        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactionsAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return Unauthorized();

            var result = await _tradingService.GetTransactionsAsync(user.Id);
            if (!result.IsSuccess) return BadRequest(result.Error);

            var transactionsDto = result.Data.Select(t => new TransactionsDto
            {
                Id = t.Id,
                StockSymbol = t.StockSymbol,
                Quantity = t.Quantity,
                Price = t.Price,
                Type = t.Type,
                Date = t.Date
            });

            return Ok(transactionsDto);
        }
        [HttpGet("stocks")]
        public async Task<IActionResult> GetStocksAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return Unauthorized();

            var result = await _tradingService.GetProfileBoughtStocks(user);
            if (!result.IsSuccess) return BadRequest(result.Error);

            return Ok(result.Data);
        }
    }
}
