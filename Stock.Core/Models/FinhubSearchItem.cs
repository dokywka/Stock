using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StockApp.Core.Models
{
    public class FinhubSearchItem
    {
        [JsonPropertyName("symbol")]
        public string Symbol {  get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
