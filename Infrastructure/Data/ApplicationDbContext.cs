using Domain.Entities.Roles;
using Domain.Entities.User;
using Infrastructure.DbEntityConfig;
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
        public DbSet<UserDetails> UserDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());

        }

    }
}
