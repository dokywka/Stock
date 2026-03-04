using StockApp.StockApp.Core.Models;
using StockApp.StockApp.Api.DTOs.Stock;
using StockApp.StockApp.Api.DTOs.Comment;

namespace StockApp.StockApp.Api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this StockItem stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(x => x.ToCommentDto()).ToList()
            };
        }
        public static StockItem ToStockFromCreateDto(this CreateStockRequestDto stockDto)
        {
            return new StockItem
            {
                Symbol= stockDto.Symbol,
                CompanyName= stockDto.CompanyName,
                Purchase= stockDto.Purchase,
                LastDiv= stockDto.LastDiv,
                Industry= stockDto.Industry,
                MarketCap= stockDto.MarketCap
            };
        }
        public static StockItem ToStockFromUpdateDto(this UpdateStockRequestDto stockDto)
        {
            return new StockItem
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
    }
}
