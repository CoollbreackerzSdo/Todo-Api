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
    private readonly TContext _context;
    private readonly DbSet<T> _table;
    private bool _disposedValue;
}