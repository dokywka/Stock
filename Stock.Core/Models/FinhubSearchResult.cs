using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StockApp.Core.Models
{
    public class FinhubSearchResult
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
        [JsonPropertyName("result")]
        public List<FinhubSearchItem> Result { get; set; }
    }
}
