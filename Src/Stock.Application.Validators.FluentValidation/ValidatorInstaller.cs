using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Stock.Application.Validators.FluentValidation;

public static class ValidatorInstaller
{
    public static IServiceCollection InstallValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}