using Framework.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Stock.Application.Features.Customers.Repositories;
using Stock.Domain.Models.Customers;

namespace Stock.Infrastructure.Pg.Ef.Domain.Customers;

internal static class CustomerRepositoryInstaller
{
    internal static IServiceCollection InstallCustomer(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IReadRepository<Customer>, CustomerRepository>();
        services.AddScoped<ICreateRepository<Customer>, CustomerRepository>();
        services.AddScoped<IUpdateRepository<Customer>, CustomerRepository>();
        services.AddScoped<IDeleteRepository<Customer>, CustomerRepository>();
        return services;
    }
}