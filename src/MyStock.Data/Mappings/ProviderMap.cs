using Microsoft.EntityFrameworkCore;
using MyStock.Business.Models;

namespace MyStock.Data.Mappings
{
    public class ProviderMap : IEntityTypeConfiguration<Provider>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.DocumentNumber).HasMaxLength(14).IsRequired();
            builder.HasMany(x => x.Products).WithOne(x => x.Provider).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
