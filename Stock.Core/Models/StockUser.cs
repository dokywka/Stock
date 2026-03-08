using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using StockApp.Core.Models;

namespace StockApp.StockApp.Core.Models
{
    public class StockUser: IdentityUser
    {
        public decimal Balance {  get; set; }
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
