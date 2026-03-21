using Microsoft.AspNetCore.Mvc;
using StockApp.Core.Common;
using StockApp.Core.Interfaces;
using StockApp.Core.Models;
using StockApp.Infrastructure;
using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StockApp.Api.Controllers
{
    [ApiController]
    [Route("Api/Recommendation")]
    public class RecommendationController: ControllerBase
    {
        private readonly IRecommendationService _recommendationService;
        public RecommendationController(IRecommendationService recommendationService) {
        _recommendationService = recommendationService;
        }

        [HttpGet]
        [Route("recommendation/{ticker}")]
        public async Task<IActionResult> GetAiRecommendationAsync([FromRoute]string ticker)
        {
            Result<AiRecommendation> recommendation = await _recommendationService.GetRecommendationByStock(ticker);
                if (!recommendation.IsSuccess) return BadRequest(recommendation.Error);

            return Ok(recommendation.Data);
        }
    }
}
