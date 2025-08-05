using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models.Suppliers;

namespace Stock.Infrastructure.Pg.Ef;

public sealed class StockDbContext(DbContextOptions<StockDbContext> options) : DbContext(options)
{
    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StockDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}