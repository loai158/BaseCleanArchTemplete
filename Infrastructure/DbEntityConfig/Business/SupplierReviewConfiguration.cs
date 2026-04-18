using Domain.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbEntityConfig.Business
{
    public class SupplierReviewConfiguration : BaseEntityConfiguration<Domain.Entities.Business.SupplierReview>
    {
        public override void Configure(EntityTypeBuilder<SupplierReview> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Rating)
                .IsRequired();

            builder.Property(x => x.Comment)
                .HasMaxLength(1000);

            // 🔗 Supplier
            builder.HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔗 Retailer
            builder.HasOne(x => x.Retailer)
                .WithMany()
                .HasForeignKey(x => x.RetailerId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔥 Constraint (1-5 rating)
            builder.HasCheckConstraint("CK_SupplierReview_Rating", "[Rating] BETWEEN 1 AND 5");

            // 🔥 Prevent duplicate reviews
            builder.HasIndex(x => new { x.SupplierId, x.RetailerId })
                .IsUnique();
        }
    }
}
