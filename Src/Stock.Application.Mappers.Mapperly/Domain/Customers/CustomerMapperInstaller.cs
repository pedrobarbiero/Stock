using Framework.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Stock.Application.Features.Customers.Requests;
using Stock.Application.Features.Customers.Responses;
using Stock.Domain.Models.Customers;

namespace Stock.Application.Mappers.Mapperly.Domain.Customers;

internal static class CustomerMapperInstaller
{
    internal static IServiceCollection InstallCustomer(this IServiceCollection services)
    {
        services.AddScoped<IMapper<Customer, CustomerResponse, CreateCustomerRequest>, CreateCustomerMapper>();
        services.AddScoped<IMapper<Customer, CustomerResponse, UpdateCustomerRequest>, UpdateCustomerMapper>();
        return services;
    }
}