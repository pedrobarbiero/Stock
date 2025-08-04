namespace Framework.Application.Repositories;

public interface IUpdateRepository<T>
{
    T Update(T aggregateRoot);
}