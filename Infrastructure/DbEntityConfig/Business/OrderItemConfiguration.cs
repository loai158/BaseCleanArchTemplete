using Domain.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbEntityConfig.Business
{
    public class OrderItemConfiguration : BaseEntityConfiguration<Domain.Entities.Business.OrderItem>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();

            // 🔗 Order
            builder.HasOne(x => x.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔗 Product
            builder.HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔗 Supplier (Multi-vendor 🔥)
            builder.HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔥 Constraint مهم
            builder.HasCheckConstraint("CK_OrderItem_Quantity", "[Quantity] > 0");

            // 🔍 Indexes
            builder.HasIndex(x => x.OrderId);
            builder.HasIndex(x => x.SupplierId);
        }
    }
}
