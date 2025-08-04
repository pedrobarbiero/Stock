using Microsoft.Extensions.DependencyInjection;
using Stock.Application.Mappers.Mapperly.Domain.Suppliers;

namespace Stock.Application.Mappers.Mapperly;

public static class MapperInstaller
{
    public static IServiceCollection InstallMappers(this IServiceCollection services)
    {
        services.InstallSupplier();
        return services;
    }
}