using Microsoft.Extensions.DependencyInjection;
using Stock.Application.Features.Suppliers.Services;

namespace Stock.Application.Features.Suppliers.Installers;

public static class SupplierInstaller
{
    public static IServiceCollection InstallSupplierServices(this IServiceCollection services)
    {
        services.AddScoped<ISupplierService, SupplierService>();
        
        
        return services;
    }
}