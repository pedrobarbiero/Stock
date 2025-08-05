using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain.Models.Customers;

namespace Stock.Infrastructure.Pg.Ef.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customers");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasValueGenerator<GuidV7ValueGenerator>();
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(CustomerConstants.MaxNameLength);
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(CustomerConstants.MaxEmailLength);

        builder.OwnsMany(x => x.Addresses, a =>
        {
            a.ToTable("customer_addresses");
            a.WithOwner().HasForeignKey("CustomerId");
            a.HasKey(c => c.Id);
            a.Property<Guid>("Id").HasValueGenerator<GuidV7ValueGenerator>();
            a.Property(x => x.Street).HasMaxLength(300);
            a.Property(x => x.City).HasMaxLength(100);
            a.Property(x => x.PostalCode).HasMaxLength(20);
        });
    }
}