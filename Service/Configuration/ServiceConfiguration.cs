using Domain.Abstracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Implemetations;

namespace Service.Configuration
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddServiceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
