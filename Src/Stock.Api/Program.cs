using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Stock.Api.Middleware;
using Stock.Application;
using Stock.Application.Mappers.Mapperly;
using Stock.Application.Validators.FluentValidation;
using Stock.Domain.Models.Customers;
using Stock.Domain.Models.Suppliers;
using Stock.Infrastructure.Pg.Ef;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddOData(options => options
    .Select()
    .Filter()
    .OrderBy()
    .Count()
    .SetMaxTop(100)
    .AddRouteComponents("odata", GetEdmModel()));
builder.Services.InstallRepositories(builder.Configuration);
builder.Services.InstallValidators();
builder.Services.InstallMappers();
builder.Services.InstallApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSingleton(TimeProvider.System);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<StockDbContext>();
    dbContext.Database.Migrate();
}

app.UseMiddleware<RequestResultMiddleware>();
app.UseMiddleware<AutoSaveChangesMiddleware>();
app.MapControllers();
app.Run();

static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<Supplier>("Suppliers");

    var customerEntitySet = builder.EntitySet<Customer>("Customers");
    var customerEntity = customerEntitySet.EntityType;

    var addressEntitySet = builder.EntitySet<CustomerAddress>("CustomerAddresses");
    var addressEntity = addressEntitySet.EntityType;

    // Explicitly configure the navigation property
    customerEntity.HasMany(c => c.Addresses);

    return builder.GetEdmModel();
}