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
            // Order model
            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderID);

            modelBuilder.Entity<Order>()
                .Property(o => o.CustomerName)
                .HasMaxLength(100);

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderDate)
                .HasColumnType("date");

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderDetails model
            modelBuilder.Entity<OrderDetails>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<OrderDetails>()
                .Property(o => o.ItemName)
                .HasMaxLength(100);

            modelBuilder.Entity<OrderDetails>()
                .Property(o => o.Price)
                .HasColumnType("float(24)");

            modelBuilder.Entity<OrderDetails>()
                .Property(o => o.Quantity)
                .HasColumnType("int");

            modelBuilder.Entity<OrderDetails>()
               .Property(o => o.Total)
               .HasColumnType("float(24)")
               .HasComputedColumnSql("[Quantity] * [Price]");

            modelBuilder.Entity<OrderDetails>()
                .HasOne(o => o.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // when principle entity deleted that time dependent entities automatically deleted.
        }

    }
}
