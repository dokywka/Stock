using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StockApp.Core.Models
{
    public class OpenRouterResponse
    {
        [JsonPropertyName("choices")]
        public List<OpenRouterChoice> Choices {  get; set; }
    }
}
