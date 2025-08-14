using System.Linq.Expressions;

namespace Framework.Application.BackgroundJobs;

public interface IBackgroundJobScheduler
{
    string? Enqueue<T>(Expression<Func<T, Task>> methodCall);
    string? Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay);
    bool Delete(string jobId);
}