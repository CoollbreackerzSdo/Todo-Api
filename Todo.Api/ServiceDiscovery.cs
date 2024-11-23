using System.Security.Claims;

using EntityFramework.Exceptions.PostgreSQL;

using Todo.Api.Account.Context;
using Todo.Api.Account.Context.Repository;
using Todo.Api.Account.Handlers.Create;

namespace Todo.Api;

public static class ServiceDiscovery
{
    public static IHostApplicationBuilder AddDbContexts(this IHostApplicationBuilder builder)
    {
        //Production contexts
        builder.AddNpgsqlDbContext<AccountContext>("todo-db", null, options =>
        {
            options.UseExceptionProcessor();
            options.UseNpgsql();
        });
        //Dev contexts
        // builder.Services.AddDbContext<AccountContext>(options =>
        // {
        //     options.UseExceptionProcessor();
        //     options.UseNpgsql();
        // });
        return builder;
    }
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddTransient<IHandlerAsync<SignUpRequest, IEnumerable<Claim>>, SigUpHandler>();
        return services;
    }
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        return services;
    }
    public static void MapMigrations(this WebApplication app)
    {
        app.Services.CreateScope().ServiceProvider.GetRequiredService<AccountContext>().Database.Migrate();
    }
}