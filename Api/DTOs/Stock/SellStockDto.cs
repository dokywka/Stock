using Microsoft.AspNetCore.Mvc;

namespace StockApp.Api.DTOs.Stock
{
    public class SellStockDto
    {
        public int StockId { get; set; }
        public int Quantity { get; set; }
    }
}
