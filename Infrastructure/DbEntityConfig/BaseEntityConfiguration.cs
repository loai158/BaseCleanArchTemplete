using Domain.Entities.BaseEntity;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DbEntityConfig
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(e => e.CreatedById)
                  .IsRequired(false)
                  .HasMaxLength(450);

            // Configure UpdatedById
            builder.Property(e => e.UpdatedById)
                .IsRequired(false)
                .HasMaxLength(450);

            builder.HasOne<ApplicationUser>(e => e.CreatedBy)
                .WithMany()
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne<ApplicationUser>(e => e.UpdatedBy)
                .WithMany()
                .HasForeignKey(e => e.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}
