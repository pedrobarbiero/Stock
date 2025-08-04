using Stock.Application.Features.Suppliers.Repositories;
using Stock.Domain.Models.Suppliers;
using Stock.Infrastructure.Pg.Ef.Repositories;

namespace Stock.Infrastructure.Pg.Ef.Domain.Suppliers;

public class SupplierRepository(StockDbContext context) : BaseRepository<Supplier>(context), ISupplierRepository;