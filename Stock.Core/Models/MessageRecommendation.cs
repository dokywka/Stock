using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StockApp.Core.Models
{
    public class MessageRecommendation
    {
        [JsonPropertyName("role")]
        public string UserRole {  get; set; }
        [JsonPropertyName("content")]
        public string Content {  get; set; }
    }
}
