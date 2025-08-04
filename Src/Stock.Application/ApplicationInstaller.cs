using Microsoft.Extensions.DependencyInjection;
using Stock.Application.Features.Suppliers.Installers;

namespace Stock.Application;

public static class ApplicationInstaller
{
    public static IServiceCollection InstallApplicationServices(this IServiceCollection services)
    {
        services.InstallSupplierServices();

        return services;
    }
}