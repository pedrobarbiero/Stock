using Framework.Application.Events;

using Microsoft.Extensions.DependencyInjection;

namespace Framework.Application;

public static class Installer
{
    public static IServiceCollection InstallEventDispatcher(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<BackgroundDomainEventProcessor>();
        return services;
    }
}