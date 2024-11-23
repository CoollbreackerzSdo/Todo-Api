namespace Todo.Api.Common.Context.Repository;

public interface IRepository<T> : IDisposable
{
    IQueryable<T> GetAll();
    void Add(T model);
}