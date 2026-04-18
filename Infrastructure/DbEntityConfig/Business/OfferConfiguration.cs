using Domain.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbEntityConfig.Business
{
    public class OfferConfiguration : BaseEntityConfiguration<Domain.Entities.Business.Offer>
    {
        public override void Configure(EntityTypeBuilder<Offer> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DiscountPercentage)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.Property(x => x.StartDate)
                .IsRequired();

            builder.Property(x => x.EndDate)
                .IsRequired();

            // 🔗 Product (optional)
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔗 Supplier (optional)
            builder.HasOne<Supplier>()
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔥 Constraint
            builder.HasCheckConstraint("CK_Offer_Discount", "[DiscountPercentage] > 0 AND [DiscountPercentage] <= 100");

            builder.HasCheckConstraint("CK_Offer_Date", "[EndDate] > [StartDate]");

            // 🔍 Indexes
            builder.HasIndex(x => x.ProductId);
            builder.HasIndex(x => x.SupplierId);
        }
    }
}
