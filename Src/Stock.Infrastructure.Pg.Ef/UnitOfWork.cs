using Framework.Application.Repositories;

namespace Stock.Infrastructure.Pg.Ef;

public sealed class UnitOfWork(StockDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        await context.SaveChangesAsync(cancellationToken);
}