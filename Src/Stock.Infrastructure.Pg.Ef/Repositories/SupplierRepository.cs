using Stock.Application.Features.Suppliers.Repositories;
using Stock.Domain.Models.Suppliers;

namespace Stock.Infrastructure.Pg.Ef.Repositories;

public class SupplierRepository(StockDbContext context) : BaseRepository<Supplier>(context), ISupplierRepository;