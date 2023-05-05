using EFCoreJsonApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJsonApp.Data
{
    public class JsonDataContext: DataContext
    {
        public DbSet<OrderWithOrderDetails> OrderWithOrderDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=OrdersDB; Integrated Security=True;")
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderWithOrderDetails>()
                .OwnsMany(x => x.OrderDetailsJson, builder => { builder.ToJson(); });
        }
    }
}
