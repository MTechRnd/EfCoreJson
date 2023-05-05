using EFCoreJsonApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreJsonApp.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=OrdersDB; Integrated Security=True;")
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderDetails>()
                .Property(o => o.Total)
                .HasComputedColumnSql("[Quantity] * [Price]");
        }

        //protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        //{
        //    configurationBuilder.Properties<List<string>>().HaveConversion<StringListConverter>();
        //}

        //private class StringListConverter: ValueConverter<List<string>, string>
        //{
        //    public StringListConverter(): base(v => string.Join(", ", v!), v=> v.Split(',',StringSplitOptions.TrimEntries).ToList())
        //    {

        //    }
        //}


    }
}
