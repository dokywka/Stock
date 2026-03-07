using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockApp.StockApp.Core.Models;


namespace StockApp.StockApp.Infrastructure
{
    public class AppDbContext : IdentityDbContext<StockUser>
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
