using System.Linq.Expressions;

namespace Todo.Api.Common.Context.Repository;

public interface IRepository<T> : IDisposable
{
    IQueryable<T> GetAll();
    void Add(T model);
    Task<Result> SaveChangesAsync(CancellationToken token = default);
    T? SingleOrDefault(Expression<Func<T, bool>> filter);
    T? Find(EntityKey<Guid> key);
    TResult? SingleAsMapOrDefault<TResult>(EntityKey<Guid> key, Expression<Func<T, TResult>> map);
}