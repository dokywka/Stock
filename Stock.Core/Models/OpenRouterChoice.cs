using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StockApp.Core.Models
{
    public class OpenRouterChoice
    {
        [JsonPropertyName("message")]
        public OpenRouterMessage Message {  get; set; }
    }
}
