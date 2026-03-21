using System;
using System.Collections.Generic;
using System.Text;

namespace StockApp.Core.Models
{
    public class ModelRecommendation
    {
        public string Model {  get; set; }
        public List<MessageRecommendation> RecommendationsList {  get; set; }
    }
}
