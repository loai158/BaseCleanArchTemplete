using Domain.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbEntityConfig.Business
{
    public class OrderConfiguration : BaseEntityConfiguration<Domain.Entities.Business.Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TotalPrice)
                .HasPrecision(18, 2);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            //  Retailer
            builder.HasOne(x => x.Retailer)
                .WithMany(r => r.Orders)
                .HasForeignKey(x => x.RetailerId)
                .OnDelete(DeleteBehavior.Restrict);

            //  Index
            builder.HasIndex(x => x.CreatedAt);
        }
    }
}
