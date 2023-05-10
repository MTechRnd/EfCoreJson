using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreJsonApp.Models.OrderDetails
{
    public class OrderDetailEntityConfiguration : IEntityTypeConfiguration<OrderDetailEntity>
    {
        public void Configure(EntityTypeBuilder<OrderDetailEntity> modelBuilder)
        {
            modelBuilder.ToTable("OrderDetails");

            modelBuilder.Property(o => o.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .IsRequired();

            modelBuilder.Property(o => o.OrderId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            modelBuilder.Property(o => o.ItemName)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            modelBuilder.Property(o => o.Price)
                .HasColumnType("float(24)");

            modelBuilder.Property(o => o.Quantity)
                .HasColumnType("int");

            modelBuilder.Property(o => o.Total)
               .HasColumnType("float(24)")
               .HasComputedColumnSql("[Quantity] * [Price]");

            modelBuilder.HasOne(o => o.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
