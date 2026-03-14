using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StockApp.Core.Models
{
    public class FinnhubQuote
    {
        [JsonPropertyName("c")]
        public decimal CurrentPrice { get; set; }

        [JsonPropertyName("h")]
        public decimal HighestPrice { get; set; }
        [JsonPropertyName("l")]
        public decimal LowestPrice { get; set; }
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
    }
}
