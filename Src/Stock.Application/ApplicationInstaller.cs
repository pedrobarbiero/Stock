using Framework.Application.Events;

using Microsoft.Extensions.DependencyInjection;

using Stock.Application.Features.Customers.Events;
using Stock.Application.Features.Customers.Services;
using Stock.Application.Features.Suppliers.Services;
using Stock.Domain.Models.Customers.DomainEvents;

namespace Stock.Application;

public static class ApplicationInstaller
{
    public static IServiceCollection InstallApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<ICustomerService, CustomerService>();

        services.AddScoped<IDomainEventHandler<CustomerUpdatedEvent>, CustomerUpdatedEventHandler>();

        return services;
    }
}