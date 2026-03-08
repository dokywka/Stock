using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockApp.Api.Controllers;
using StockApp.Api.DTOs.Stock;
using StockApp.Core.Interfaces;
using StockApp.StockApp.Core.Models;

namespace StockApp.StockApp.Api.Controllers
{
    [Route("Api/Portfolio")]
    [ApiController]
    public class PortfolioController:BaseController
    {
        private readonly ITransactionsPortfolioRepository _portfolioRepository;
        public PortfolioController(UserManager<StockUser> userManager, ITransactionsPortfolioRepository portfolioRepository) : base(userManager)
        {
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPortfolioAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return Unauthorized();

            var allStocks = await _portfolioRepository.GetAllPortfolioTransactionsAsync(user);
            return Ok(allStocks);

        }
        [HttpGet("value")]
        public async Task<IActionResult> GetPorfolioValueAsync()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return Unauthorized();

            var portfolioCost=await _portfolioRepository.GetPortfolioValueAsync(user);
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

            decimal? tryBuyTransaction=await _portfolioRepository.BuyTransactionToPortfolioAsync(user,stockId,quantity);
            if (tryBuyTransaction==null) return BadRequest("Недостаточно средств или акция не найдена");

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

            decimal? trySellTransaction = await _portfolioRepository.SellTransactionFromPortfolioAsync(user, stockId, amount);

            if (trySellTransaction==null) return BadRequest("Акция не найдена или недостаточное количество");

            return Ok($"Акция успешно продана, выгода {trySellTransaction}");
        }
    }
}
