using EFCoreJsonApp.Models.OrderDetails;
using EFCoreJsonApp.Models.OrderWithOrderDetail;
using EFCoreJsonApp.Models.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCoreJsonApp.Data
{
    public class JsonDataContext: DbContext
    {
        public DbSet<OrderWithOrderDetailEntity> OrderWithOrderDetails { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration config = new ConfigurationBuilder()
                 .AddUserSecrets<JsonDataContext>()
                 .Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderWithOrderDetailEntityConfiguration());
            modelBuilder.Entity<TotalPriceResult>().ToTable("TotalPriceResult", t => t.ExcludeFromMigrations()).HasNoKey();
            modelBuilder.Entity<TotalQuantityResult>().ToTable("TotalQuantityResult", t => t.ExcludeFromMigrations()).HasNoKey();
            modelBuilder.Entity<AverageOfPriceResult>().ToTable("AverageOfPriceResult", t => t.ExcludeFromMigrations()).HasNoKey();
            modelBuilder.Entity<AverageOfQuantityResult>().ToTable("AverageOfQuantityResult", t => t.ExcludeFromMigrations()).HasNoKey();
            modelBuilder.Entity<MaxQuantityResult>().ToTable("MaxQuantity", t => t.ExcludeFromMigrations()).HasNoKey();
            modelBuilder.Entity<MinQuantityResult>().ToTable("MinQuantity", t => t.ExcludeFromMigrations()).HasNoKey();
            modelBuilder.Entity<TotalByOrderResult>().ToTable("TotalByOrder", t => t.ExcludeFromMigrations()).HasNoKey();
            modelBuilder.Entity<MaxPriceResult>().ToTable("MaxPrice", t => t.ExcludeFromMigrations()).HasNoKey();
            modelBuilder.Entity<MinPriceResult>().ToTable("MinPrice", t => t.ExcludeFromMigrations()).HasNoKey();
            modelBuilder.Entity<TotalOrderByCustomerResult>().ToTable("TotalOrderByCustomerResult", t => t.ExcludeFromMigrations()).HasNoKey();
            modelBuilder.Entity<OrderCount>().ToTable("OrderCount", t => t.ExcludeFromMigrations()).HasNoKey();
        }
    }
}
