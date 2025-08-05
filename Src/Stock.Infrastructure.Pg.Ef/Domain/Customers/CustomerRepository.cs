using Stock.Application.Features.Customers.Repositories;
using Stock.Domain.Models.Customers;
using Stock.Infrastructure.Pg.Ef.Repositories;

namespace Stock.Infrastructure.Pg.Ef.Domain.Customers;

public class CustomerRepository(StockDbContext context) : BaseRepository<Customer>(context), ICustomerRepository;