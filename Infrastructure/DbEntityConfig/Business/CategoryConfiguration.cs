using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbEntityConfig.Business
{
    public class CategoryConfiguration : BaseEntityConfiguration<Domain.Entities.Business.Category>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.Business.Category> builder)
        {
            base.Configure(builder);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(x => x.Parent)
               .WithMany(x => x.Children)
               .HasForeignKey(x => x.ParentId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.Name);
        }
    }
}
