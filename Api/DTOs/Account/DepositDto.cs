using StockApp.StockApp.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace StockApp.Api.DTOs.Account
{
    public class DepositDto
    {
        [Required]
        [Range(1, 100000)]
        public decimal DepositAmount { get; set; }
    }
}
