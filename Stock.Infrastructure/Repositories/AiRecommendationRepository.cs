using AiStockAdvisor.Models;
using StockApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StockApp.StockApp.Infrastructure;

namespace StockApp.Infrastructure.Repositories
{
    public class AiRecommendationRepository:IAiRecommendationRepository
    {
        private readonly AppDbContext _context;

        public AiRecommendationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AiRecommendation?> GetCurrentAsync(string symbol, int maxAgeHours = 6)
        {
            var cutoffTime = DateTime.UtcNow.AddHours(-maxAgeHours);

            return await _context.AiRecommendations
                .Include(r => r.StockItem)
                .Where(r => r.Symbol == symbol && r.CreatedAt >= cutoffTime)
                .OrderByDescending(r => r.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task SaveAsync(AiRecommendation recommendation)
        {
            recommendation.CreatedAt = DateTime.UtcNow;
            recommendation.ExpiresAt = DateTime.UtcNow.AddHours(6);

            _context.AiRecommendations.Add(recommendation);
            await _context.SaveChangesAsync();
        }
    }
}
