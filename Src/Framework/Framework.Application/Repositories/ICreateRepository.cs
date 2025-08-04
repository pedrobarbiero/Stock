namespace Framework.Application.Repositories;

public interface ICreateRepository<T>
{
    T Add(T aggregateRoot);
}