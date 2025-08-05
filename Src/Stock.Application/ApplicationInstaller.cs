using Microsoft.Extensions.DependencyInjection;
using Stock.Application.Features.Customers.Services;
using Stock.Application.Features.Suppliers.Services;

namespace Stock.Application;

public static class ApplicationInstaller
{
    public static IServiceCollection InstallApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }
}