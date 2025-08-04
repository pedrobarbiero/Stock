using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Stock.Validations.FluentValidation;

public static class ValidatorInstaller
{
    public static IServiceCollection InstallValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}