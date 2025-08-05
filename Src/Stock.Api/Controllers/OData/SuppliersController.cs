using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Stock.Domain.Models.Suppliers;
using Stock.Infrastructure.Pg.Ef;

namespace Stock.Api.Controllers.OData;

public class SuppliersController(StockDbContext context) : ODataController
{
    [EnableQuery]
    [HttpGet]
    public IQueryable<Supplier> Get()
    {
        return context.Suppliers;
    }

    [EnableQuery]
    [HttpGet]
    public SingleResult<Supplier> Get([FromODataUri] Guid key)
    {
        var supplier = context.Suppliers.Where(s => s.Id == key);

        return SingleResult.Create(supplier);
    }
}