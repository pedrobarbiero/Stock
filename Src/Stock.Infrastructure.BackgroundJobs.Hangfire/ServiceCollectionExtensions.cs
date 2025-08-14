using Framework.Application.BackgroundJobs;

using Hangfire;
using Hangfire.PostgreSql;

using Microsoft.Extensions.DependencyInjection;

namespace Stock.Infrastructure.BackgroundJobs.Hangfire;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHangfireBackgroundJobs(this IServiceCollection services,
        string connectionString)
    {
        services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(c => c.UseNpgsqlConnection(connectionString), new PostgreSqlStorageOptions
            {
                QueuePollInterval = TimeSpan.FromSeconds(15),
                SchemaName = "hangfire"
            }));

        services.AddHangfireServer();

        services.AddScoped<IBackgroundJobScheduler, HangfireBackgroundJobScheduler>();

        return services;
    }
}