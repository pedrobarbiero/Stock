using Framework.Application.Repositories;
using Stock.Domain.Models.Suppliers;

namespace Stock.Application.Features.Suppliers.Repositories;

public interface ISupplierRepository : IReadRepository<Supplier>, ICreateRepository<Supplier>,
    IUpdateRepository<Supplier>,
    IDeleteRepository<Supplier>;