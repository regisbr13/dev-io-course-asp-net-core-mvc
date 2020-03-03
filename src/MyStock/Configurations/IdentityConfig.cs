using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyStock.Data;

namespace MyStock.Configurations
{
    public static class IdentityConfig
    {
        public static IServiceCollection ResolveIdentityDependencies(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<MyIdentityDbContext>();

            return services;
        }
    }
}