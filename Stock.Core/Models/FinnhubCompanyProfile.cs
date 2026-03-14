using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace StockApp.Core.Models
{
    public class FinnhubCompanyProfile
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("finnhubIndustry")]
        public string FinnhubIndustry {  get; set; }
        [JsonPropertyName("marketCapitalization")]
        public decimal MarketCapitalization {  get; set; }
        [JsonPropertyName("ticker")]
        public string Ticker {  get; set; }
    }
}
