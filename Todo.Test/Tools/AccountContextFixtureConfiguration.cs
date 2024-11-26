using EntityFramework.Exceptions.Sqlite;

using Microsoft.EntityFrameworkCore;

using Todo.Api.Account.Context;

namespace Todo.Test.Tools;

public class AccountContextFixtureConfiguration : IDisposable
{
    public AccountContextFixtureConfiguration()
    {
        var options = new DbContextOptionsBuilder<AccountContext>()
            .UseInMemoryDatabase("Movie")
            .UseExceptionProcessor();
        _context = new(options.Options);
    }
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
    // ~AccountContextFixtureConfiguration()
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
    private bool _disposedValue;
    private readonly AccountContext _context;
    public static implicit operator AccountContext(AccountContextFixtureConfiguration configuration) => configuration._context;
}