using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain.Models.Customers;

namespace Stock.Infrastructure.Pg.Ef.Configurations;

public class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
{
    public void Configure(EntityTypeBuilder<CustomerAddress> builder)
    {
        builder.ToTable("customer_addresses");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Street).HasMaxLength(300);
        builder.Property(x => x.City).HasMaxLength(100);
        builder.Property(x => x.PostalCode).HasMaxLength(20);
    }
}