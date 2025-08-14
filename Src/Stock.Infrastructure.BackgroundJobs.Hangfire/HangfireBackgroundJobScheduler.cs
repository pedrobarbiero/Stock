using System.Linq.Expressions;

using Framework.Application.BackgroundJobs;

using Hangfire;

namespace Stock.Infrastructure.BackgroundJobs.Hangfire;

public class HangfireBackgroundJobScheduler(IBackgroundJobClient backgroundJobClient) : IBackgroundJobScheduler
{
    private readonly IBackgroundJobClient _backgroundJobClient =
        backgroundJobClient ?? throw new ArgumentNullException(nameof(backgroundJobClient));

    public string? Enqueue<T>(Expression<Func<T, Task>> methodCall)
        => _backgroundJobClient.Enqueue(methodCall);

    public string? Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay) =>
        _backgroundJobClient.Schedule(methodCall, delay);

    public bool Delete(string jobId) => _backgroundJobClient.Delete(jobId);
}