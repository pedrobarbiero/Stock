using Framework.Application.Validation;
using Microsoft.Extensions.DependencyInjection;
using Stock.Application.Features.Customers.Requests;

namespace Stock.Application.Validators.FluentValidation.Domain.Customers;

internal static class CustomerValidatorInstaller
{
    internal static IServiceCollection InstallCustomer(this IServiceCollection services)
    {
        services.AddScoped<IRequestValidator<CreateCustomerRequest>, CreateCustomerRequestValidator>();
        services.AddScoped<IRequestValidator<UpdateCustomerRequest>, UpdateCustomerRequestValidator>();

        return services;
    }
}