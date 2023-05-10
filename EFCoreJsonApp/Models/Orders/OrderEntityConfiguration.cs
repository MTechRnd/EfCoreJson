using EFCoreJsonApp.Models.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreJsonApp.Models.Orders
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> modelBuilder)
        {
            modelBuilder.ToTable("Orders");

            modelBuilder.Property(o => o.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .IsRequired();

            modelBuilder.Property(o => o.CustomerName)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            modelBuilder.Property(o => o.OrderDate)
                .HasColumnType("date");

            modelBuilder.HasMany(o => o.OrderDetails)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
