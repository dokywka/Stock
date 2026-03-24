using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using StockApp.Core.Models;
using StockApp.StockApp.Core.Models;
using StockApp.Core.Models;


namespace StockApp.StockApp.Infrastructure
{
    public class AppDbContext : IdentityDbContext<StockUser>
    {
        public DbSet<StockItem> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<StockTransaction> Transactions {  get; set; }
        public DbSet<AiRecommendation> AiRecommendations { get; set; }

        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options):
            base (options) { 
        
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Portfolio>(entity=> {
                entity.HasKey(x=>x.PortfolioId);

                entity.HasOne(x => x.User).WithMany(x => x.Portfolios).HasForeignKey(x=>x.UserId);

                });

            modelBuilder.Entity<Portfolio>(entity =>
            {
                entity.HasKey(x => x.PortfolioId);

                entity.HasOne(x => x.Stock).WithMany(x => x.Portfolios).HasForeignKey(x => x.StockId);

            });

            modelBuilder.Entity<StockTransaction>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.User).WithMany(x => x.Transactions).HasForeignKey(x => x.UserId);
            });

        }
    }
}
