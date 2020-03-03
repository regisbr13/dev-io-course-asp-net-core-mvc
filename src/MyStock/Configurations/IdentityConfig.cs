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

            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";	
                options.User.RequireUniqueEmail = true;		
                options.Password.RequireDigit = false;								
                options.Password.RequiredLength = 6;									
                options.Password.RequireLowercase = false;								
                options.Password.RequireUppercase = false;								
                options.Password.RequireNonAlphanumeric = false;						
                options.Password.RequiredUniqueChars = 3;				
            });
            return services;
        }
    }
}