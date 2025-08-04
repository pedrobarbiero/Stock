using Framework.Application.Validation;
using Microsoft.Extensions.DependencyInjection;
using Stock.Application.Features.Suppliers.Requests;

namespace Stock.Application.Validators.FluentValidation.Domain.Supplier;

internal static class SupplierValidatorInstaller
{
    internal static IServiceCollection InstallSupplier(this IServiceCollection services)
    {
        services.AddScoped<IRequestValidator<CreateSupplierRequest>, CreateSupplierValidator>();
        services.AddScoped<IRequestValidator<UpdateSupplierRequest>, UpdateSupplierValidator>();

        return services;
    }
}