using Microsoft.EntityFrameworkCore;
using Stock.Api.Middleware;
using Stock.Application;
using Stock.Application.Mappers.Mapperly;
using Stock.Application.Validators.FluentValidation;
using Stock.Infrastructure.Pg.Ef;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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
app.MapControllers();
app.Run();