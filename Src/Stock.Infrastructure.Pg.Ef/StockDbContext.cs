using Microsoft.EntityFrameworkCore;

namespace Stock.Infrastructure.Pg.Ef;

public sealed class StockDbContext(DbContextOptions<StockDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StockDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}