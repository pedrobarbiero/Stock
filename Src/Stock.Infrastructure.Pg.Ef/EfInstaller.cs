using Framework.Application.Repositories;
using Framework.Application.Events;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Stock.Infrastructure.Pg.Ef.Domain.Customers;
using Stock.Infrastructure.Pg.Ef.Domain.Suppliers;
using Stock.Infrastructure.Pg.Ef.Interceptors;
using Stock.Infrastructure.Pg.Ef.Events;

namespace Stock.Infrastructure.Pg.Ef;

public static class EfInstaller
{
    public static IServiceCollection InstallRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<DomainEventInterceptor>();
        services.AddScoped<IDomainEventProcessor, DomainEventBackgroundJob>();
        services.AddScoped<IDomainEventHandlerRegistry, DomainEventHandlerRegistry>();
        services.AddScoped<CustomerUpdatedEventHandler>();

        services.AddDbContext<StockDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(StockDbContext).Assembly.FullName));
#if DEBUG
            options.EnableSensitiveDataLogging();
#endif
            var interceptor = serviceProvider.GetRequiredService<DomainEventInterceptor>();
            options.AddInterceptors(interceptor);
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.InstallSupplier();
        services.InstallCustomer();
        return services;
    }
}