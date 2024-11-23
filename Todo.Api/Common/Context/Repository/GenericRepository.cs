

using System.Linq.Expressions;

using EntityFramework.Exceptions.Common;

namespace Todo.Api.Common.Context.Repository;

public abstract class GenericRepository<T, TContext> : IRepository<T>
    where T : EntityBase
    where TContext : DbContext
{
    public GenericRepository(TContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }
    public void Add(T model) => _table.Add(model);
    public IQueryable<T> GetAll() => _table;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // TODO: eliminar el estado administrado (objetos administrados)
                _context.Dispose();
            }
            // TODO: liberar los recursos no administrados (objetos no administrados) y reemplazar el finalizador
            // TODO: establecer los campos grandes como NULL
            _disposedValue = true;
        }
    }

    // // TODO: reemplazar el finalizador solo si "Dispose(bool disposing)" tiene código para liberar los recursos no administrados
    // ~GenericRepository()
    // {
    //     // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
    //     Dispose(disposing: false);
    // }
    public void Dispose()
    {
        // No cambie este código. Coloque el código de limpieza en el método "Dispose(bool disposing)".
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    public async Task<Result> SaveChangesAsync(CancellationToken token = default)
    {
        try
        {
            await _context.SaveChangesAsync(token);
            return Result.Success();
        }
        catch (UniqueConstraintException e)
        {
            return Result.Conflict($"Duplicate value for {e.ConstraintProperties[0]}");
        }
        catch (Exception)
        {
            return Result.Error();
        }
    }
    public T? SingleOrDefault(Expression<Func<T, bool>> filter)
        => _table.SingleOrDefault(filter);
    public T? Find(EntityKey<Guid> key)
        => _table.Find(key);
    public TResult? SingleAsMapOrDefault<TResult>(EntityKey<Guid> key, Expression<Func<T, TResult>> map)
        => _table.AsQueryable().Where(x => x.Key == key).Select(map).SingleOrDefault();
    private protected readonly TContext _context;
    private protected readonly DbSet<T> _table;
    private bool _disposedValue;
}