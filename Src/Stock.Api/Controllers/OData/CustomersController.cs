using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Stock.Domain.Models.Customers;
using Stock.Infrastructure.Pg.Ef;

namespace Stock.Api.Controllers.OData;

public class CustomersController(StockDbContext context) : ODataController
{
    [EnableQuery]
    [HttpGet]
    public IQueryable<Customer> Get()
    {
        return context.Customers.Include(c => c.Addresses);
    }

    [EnableQuery]
    [HttpGet]
    public SingleResult<Customer> Get([FromODataUri] Guid key)
    {
        var customer = context.Customers.Include(c => c.Addresses).Where(c => c.Id == key);

        return SingleResult.Create(customer);
    }
}