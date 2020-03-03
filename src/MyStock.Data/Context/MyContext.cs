using Microsoft.EntityFrameworkCore;
using MyStock.Business.Models;
using MyStock.Data.Mappings;

namespace MyStock.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductMap());
            builder.ApplyConfiguration(new AddressMap());
            builder.ApplyConfiguration(new ProviderMap());
            builder.ApplyConfiguration(new CategoryMap());

            base.OnModelCreating(builder);
        }
    }
}
