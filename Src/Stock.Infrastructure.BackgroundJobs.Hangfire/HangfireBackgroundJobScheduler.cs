using System.Linq.Expressions;

using Framework.Application.BackgroundJobs;

using Hangfire;

namespace Stock.Infrastructure.BackgroundJobs.Hangfire;

public class HangfireBackgroundJobScheduler(IBackgroundJobClient backgroundJobClient) : IBackgroundJobScheduler
{
    private readonly IBackgroundJobClient _backgroundJobClient =
        backgroundJobClient ?? throw new ArgumentNullException(nameof(backgroundJobClient));

    public Guid Enqueue<T>(Expression<Func<T, Task>> methodCall)
    {
        var jobId = _backgroundJobClient.Enqueue(methodCall);
        return Guid.Parse(jobId);
    }

    public Guid Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay)
    {
        var jobId = _backgroundJobClient.Schedule(methodCall, delay);
        return Guid.Parse(jobId);
    }


    public bool Delete(Guid jobId)
    {
        return _backgroundJobClient.Delete(jobId.ToString());
    }
}