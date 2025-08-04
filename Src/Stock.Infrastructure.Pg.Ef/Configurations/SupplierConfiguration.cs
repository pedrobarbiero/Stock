using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain.Models.Constants;
using Stock.Domain.Models.Suppliers;

namespace Stock.Infrastructure.Pg.Ef.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(s => s.Id); 
        builder.Property(s => s.Id).HasValueGenerator<GuidV7ValueGenerator>();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(SupplierConstants.MaxNameLength);

        builder.Property(s => s.Email)
            .IsRequired()
            .HasMaxLength(SupplierConstants.MaxEmailLength);
    }
}