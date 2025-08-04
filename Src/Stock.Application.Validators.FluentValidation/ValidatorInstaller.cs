using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Stock.Application.Validators.FluentValidation.Domain.Supplier;

namespace Stock.Application.Validators.FluentValidation;

public static class ValidatorInstaller
{
    public static IServiceCollection InstallValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.InstallSupplier();

        return services;
    }
}