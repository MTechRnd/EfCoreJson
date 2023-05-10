using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreJsonApp.Models.OrderWithOrderDetail
{
    public class OrderWithOrderDetailEntityConfiguration : IEntityTypeConfiguration<OrderWithOrderDetailEntity>
    {
        public void Configure(EntityTypeBuilder<OrderWithOrderDetailEntity> modelBuilder)
        {
            modelBuilder.Property(o => o.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .IsRequired();

            modelBuilder.Property(o => o.CustomerName)
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            modelBuilder.Property(o => o.OrderDate)
                .HasColumnType("date");

            modelBuilder.OwnsMany(x => x.OrderDetailsJson, builder => { builder.ToJson(); });
        }
    }
}
