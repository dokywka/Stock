using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StockApp.Core.Models
{
    public class OpenRouterMessage
    {
        [JsonPropertyName("content")]
        public string Content {  get; set; }
    }
}
