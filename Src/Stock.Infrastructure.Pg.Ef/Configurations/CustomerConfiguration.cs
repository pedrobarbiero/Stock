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
        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(CustomerConstants.MaxNameLength);
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(CustomerConstants.MaxEmailLength);

        builder.HasMany(x => x.Addresses)
            .WithOne()
            .HasForeignKey(a => a.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}