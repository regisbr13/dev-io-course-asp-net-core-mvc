using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyStock.Business.Interfaces.Repository;
using MyStock.Data.Context;
using MyStock.Data.Repository;

namespace MyStock.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<MyContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();

            services.AddAutoMapper(typeof(Startup));

            return services;
        }
    }
}