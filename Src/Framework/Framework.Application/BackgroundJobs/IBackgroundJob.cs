namespace Framework.Application.BackgroundJobs;

public interface IBackgroundJob
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}