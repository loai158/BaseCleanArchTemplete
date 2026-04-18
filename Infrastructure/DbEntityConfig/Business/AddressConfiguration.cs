using Domain.Entities.Business;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbEntityConfig.Business
{
    public class AddressConfiguration : BaseEntityConfiguration<Domain.Entities.Business.Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Area)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Street)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Notes)
                .HasMaxLength(500);

            // 🔍 Index for searching
            builder.HasIndex(x => x.City);
            builder.HasIndex(x => x.Area);
        }
    }
}
