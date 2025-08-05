using Framework.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Stock.Application.Features.Suppliers.Repositories;
using Stock.Domain.Models.Suppliers;

namespace Stock.Infrastructure.Pg.Ef.Domain.Suppliers;

internal static class SupplierRepositoryInstaller
{
    internal static IServiceCollection InstallSupplier(this IServiceCollection services)
    {
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IReadRepository<Supplier>, SupplierRepository>();
        services.AddScoped<ICreateRepository<Supplier>, SupplierRepository>();
        services.AddScoped<IUpdateRepository<Supplier>, SupplierRepository>();
        services.AddScoped<IDeleteRepository<Supplier>, SupplierRepository>();
        return services;
    }
}