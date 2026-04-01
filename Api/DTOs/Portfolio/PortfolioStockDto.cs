namespace StockApp.Api.DTOs.Portfolio
{
    public class PortfolioStockDto
    {
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public decimal Purchase { get; set; }
        public int Quantity { get; set; }
    }
}
