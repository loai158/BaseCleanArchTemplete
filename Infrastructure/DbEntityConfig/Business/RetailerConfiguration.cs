using Domain.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbEntityConfig.Business
{
    public class RetailerConfiguration : BaseEntityConfiguration<Domain.Entities.Business.Retailer>
    {
        public override void Configure(EntityTypeBuilder<Retailer> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ShopName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            // 🔗 User (1:1)
            builder.HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<Retailer>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
