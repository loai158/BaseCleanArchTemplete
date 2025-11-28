using Domain.Entities.Roles;
using Domain.Entities.User;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration
{
    public static class InfrastructureServiceConfiguration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Data.ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            })
              .AddEntityFrameworkStores<Data.ApplicationDbContext>()
              .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
