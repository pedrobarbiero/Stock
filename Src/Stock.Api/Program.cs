using Stock.Application;
using Stock.Validations.FluentValidation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.InstallValidators();
builder.Services.InstallApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSingleton(TimeProvider.System);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();