using StockApp.StockApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockApp.Core.Models
{
    public  class Portfolio
    {
        public int PortfolioId { get; set; }
        public string UserId {  get; set; }//т.к. строка для Identity 
        public int StockId {  get; set; }
        public int Quantity {  get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime PurchaseDate {  get; set; }
        public StockUser User { get; set; }
        public StockItem Stock { get; set; }

    }
}
