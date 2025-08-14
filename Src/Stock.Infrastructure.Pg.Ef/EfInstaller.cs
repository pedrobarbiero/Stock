using Framework.Application.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Stock.Infrastructure.Pg.Ef.Domain.Customers;
using Stock.Infrastructure.Pg.Ef.Domain.Suppliers;

namespace Stock.Infrastructure.Pg.Ef;

public static class EfInstaller
{
    public static IServiceCollection InstallRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StockDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(StockDbContext).Assembly.FullName));
#if DEBUG
            options.EnableSensitiveDataLogging();
#endif
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.InstallSupplier();
        services.InstallCustomer();
        return services;
    }
}