using Microsoft.EntityFrameworkCore;

namespace Stock.Infrastructure.Pg.Ef;

public sealed class StockDbContext(DbContextOptions<StockDbContext> options) : DbContext(options);