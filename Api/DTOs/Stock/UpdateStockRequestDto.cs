using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Не больше 10 биржевых символов.")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(10, ErrorMessage = "Название компании не более 10 символов.")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 10000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Индустрия не более 10 символов.")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 5000000000)]
        public long MarketCap { get; set; }
    }
}
