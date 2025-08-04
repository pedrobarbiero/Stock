using Framework.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Stock.Application.Features.Suppliers.Requests;
using Stock.Application.Features.Suppliers.Responses;
using Stock.Domain.Models.Suppliers;

namespace Stock.Application.Mappers.Mapperly.Domain.Suppliers;

internal static class SupplierMapperInstaller
{
    internal static IServiceCollection InstallSupplier(this IServiceCollection services)
    {
        services.AddScoped<IMapper<Supplier, SupplierResponse, CreateSupplierRequest>, CreateSupplierMapper>();
        return services;
    }
}