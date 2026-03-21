using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StockApp.Core.Models
{
    public class ModelRecommendation
    {
        [JsonPropertyName("model")]
        public string Model {  get; set; }
        [JsonPropertyName("messages")]
        public List<MessageRecommendation> RecommendationsList {  get; set; }
    }
}
