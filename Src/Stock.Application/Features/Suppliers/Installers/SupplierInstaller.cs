using Microsoft.Extensions.DependencyInjection;
using Stock.Application.Features.Suppliers.Services;

namespace Stock.Application.Features.Suppliers.Installers;

internal static class SupplierInstaller
{
    internal static IServiceCollection InstallSupplierServices(this IServiceCollection services)
    {
        services.AddScoped<ISupplierService, SupplierService>();

        return services;
    }
}