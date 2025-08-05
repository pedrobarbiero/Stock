using Framework.Application.Repositories;
using Stock.Domain.Models.Customers;

namespace Stock.Application.Features.Customers.Repositories;

public interface ICustomerRepository : IReadRepository<Customer>, ICreateRepository<Customer>,
    IUpdateRepository<Customer>,
    IDeleteRepository<Customer>;