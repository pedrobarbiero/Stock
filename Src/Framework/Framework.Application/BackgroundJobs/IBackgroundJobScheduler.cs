using System.Linq.Expressions;

namespace Framework.Application.BackgroundJobs;

public interface IBackgroundJobScheduler
{
    Guid Enqueue<T>(Expression<Func<T, Task>> methodCall);
    Guid Schedule<T>(Expression<Func<T, Task>> methodCall, TimeSpan delay);
    bool Delete(Guid jobId);
}