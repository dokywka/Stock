using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options):
            base (options) { 
        
        }
    }
}
