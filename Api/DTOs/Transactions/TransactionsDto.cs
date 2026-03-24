using StockApp.Core.Models;

namespace StockApp.Api.DTOs.Transactions
{
    public class TransactionsDto
    {
        public int Id { get; set; } 
        public string StockSymbol { get; set; }
        public int Quantity {  get; set; } 
        public decimal Price {  get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date {  get; set; }
    }
}
