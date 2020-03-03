using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyStock.Data;
using MyStock.Data.Context;

namespace MyStock.Configurations
{
    public static class DataBaseConfig
    {
        public static IServiceCollection ResolveDataBaseDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<MyIdentityDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<MyContext>();

            return services;
        }
    }
}
