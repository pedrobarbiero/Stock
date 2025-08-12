using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stock.Domain.Models.Suppliers;

namespace Stock.Infrastructure.Pg.Ef.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("suppliers");
        builder.HasKey(s => s.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(SupplierConstants.MaxNameLength);

        builder.Property(s => s.Email)
            .IsRequired()
            .HasMaxLength(SupplierConstants.MaxEmailLength);
    }
}