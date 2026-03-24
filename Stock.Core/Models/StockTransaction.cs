using StockApp.StockApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockApp.Core.Models
{
    public class StockTransaction
    {
        public int Id {  get; set; }
        public string UserId { get; set; }
        public string StockSymbol {  get; set; }
        public int Quantity {  get; set; }
        public decimal Price {  get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
        public StockUser User { get; set; }
    }
}
