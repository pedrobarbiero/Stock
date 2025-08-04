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
}

app.MapControllers();
app.Run();