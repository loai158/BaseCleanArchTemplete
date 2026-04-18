using Domain.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbEntityConfig.Business
{
    public class SupplierConfiguration : BaseEntityConfiguration<Domain.Entities.Business.Supplier>
    {
        public override void Configure(EntityTypeBuilder<Supplier> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StoreName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            // 🔗 User (1:1)
            builder.HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<Supplier>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
