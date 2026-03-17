using System;
using System.Collections.Generic;
using System.Text;
using StockApp.StockApp.Core.Models;

namespace AiStockAdvisor.Models
{
    public class AiRecommendation
    {
        public int Id { get; set; }

        // Внешний ключ к твоей таблице акций
        public int StockItemId { get; set; }
        public StockItem StockItem { get; set; } = null!;

        // Дублируем символ для быстрых запросов
        public string Symbol { get; set; } = string.Empty;

        // Результат от нейросети
        public Action ActionOn { get; set; } // Buy/Sell/Hold
        public string Explanation { get; set; } = string.Empty;
        public int Confidence { get; set; } // 0-100

        // Метаданные
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; } // CreatedAt + N часов
    }
}
