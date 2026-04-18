using Domain.Entities.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbEntityConfig.Business
{
    public class PaymentConfiguration : BaseEntityConfiguration<Domain.Entities.Business.Payment>
    {
        public override void Configure(EntityTypeBuilder<Payment> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Method)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            // 🔗 Order (1:1)
            builder.HasOne(x => x.Order)
                .WithOne()
                .HasForeignKey<Payment>(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔍 Index
            builder.HasIndex(x => x.OrderId).IsUnique();
        }
    }
}
