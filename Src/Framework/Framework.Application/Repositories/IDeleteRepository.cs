namespace Framework.Application.Repositories;

public interface IDeleteRepository<in T>
{
    void Delete(T entity);
}