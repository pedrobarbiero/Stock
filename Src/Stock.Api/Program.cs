using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

using Stock.Api.GraphQL;
using Stock.Api.Swagger;
using Stock.Api.Middleware;
using Stock.Application;
using Stock.Application.Mappers.Mapperly;
using Stock.Application.Validators.FluentValidation;
using Stock.Domain.Models.Customers;
using Stock.Domain.Models.Suppliers;
using Stock.Infrastructure.Pg.Ef;
using Stock.Infrastructure.BackgroundJobs.Hangfire;

using Hangfire;

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
builder.Services.InstallGraphQl();
builder.Services.AddHangfireBackgroundJobs(builder.Configuration.GetConnectionString("HangfireConnection")!);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "Stock API", Version = "v1", Description = "Stock management API"
        });
    c.SchemaFilter<FluentValidationRules>();
});
builder.Services.AddSingleton(TimeProvider.System);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stock API V1");
        c.RoutePrefix = "swagger";
    });

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<StockDbContext>();
    dbContext.Database.Migrate();
}

app.UseMiddleware<RequestResultMiddleware>();
app.UseMiddleware<AutoSaveChangesMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseHangfireDashboard("/hangfire");
}

app.MapControllers();
app.MapGraphQL();
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