using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyStock.Business.Models;

namespace MyStock.Data.Mappings
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Street).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Complement).HasMaxLength(50);
            builder.Property(x => x.Cep).HasMaxLength(8).IsRequired();
            builder.Property(x => x.District).HasMaxLength(50).IsRequired();
            builder.Property(x => x.City).HasMaxLength(50).IsRequired();
            builder.Property(x => x.State).HasMaxLength(2).IsRequired();
            builder.HasOne(x => x.Provider).WithOne(x => x.Address).HasForeignKey<Address>(x => x.ProviderId);
        }
    }
}
