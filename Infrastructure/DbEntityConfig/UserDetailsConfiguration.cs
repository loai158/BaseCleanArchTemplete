using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbEntityConfig
{
    public class UserDetailsConfiguration : BaseEntityConfiguration<UserDetails>
    {
        public override void Configure(EntityTypeBuilder<UserDetails> entity)
        {
            base.Configure(entity);
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
            entity.Property(e => e.LastName).HasMaxLength(50).IsRequired();
            entity.Property(e => e.UserId).HasMaxLength(450).IsRequired();
            entity.HasOne(e => e.User)
              .WithOne(u => u.UserDetails)
              .HasForeignKey<UserDetails>(e => e.UserId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
