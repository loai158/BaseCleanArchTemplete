using Domain.Entities.Roles;
using Domain.Entities.User;
using Infrastructure.DbEntityConfig;
using Infrastructure.DbEntityConfig.Business;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public ApplicationDbContext() { }
        public DbSet<Domain.Entities.User.UserDetails> UserDetails { get; set; }
        public DbSet<Domain.Entities.Business.Product> Products { get; set; }
        public DbSet<Domain.Entities.Business.Category> Categories { get; set; }
        public DbSet<Domain.Entities.Business.Offer> Offers { get; set; }
        public DbSet<Domain.Entities.Business.Supplier> Suppliers { get; set; }
        public DbSet<Domain.Entities.Business.SupplierReview> SupplierReviews { get; set; }
        public DbSet<Domain.Entities.Business.Order> Orders { get; set; }
        public DbSet<Domain.Entities.Business.OrderItem> OrderItems { get; set; }
        public DbSet<Domain.Entities.Business.Payment> Payments { get; set; }
        public DbSet<Domain.Entities.Business.Retailer> Retailers { get; set; }
        public DbSet<Domain.Entities.Business.Address> Addresses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OfferConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierReviewConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new RetailerConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
        }

    }
}
