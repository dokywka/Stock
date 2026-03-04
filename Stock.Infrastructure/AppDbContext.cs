using StockApp.StockApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace StockApp.StockApp.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<StockItem> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options):
            base (options) { 
        
        }
    }
}
