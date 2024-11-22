using EntityFramework.Exceptions.PostgreSQL;

using Todo.Api.Account.Context;

namespace Todo.Api;

public static class ServiceDiscovery
{
    public static IHostApplicationBuilder AddDbContexts(this IHostApplicationBuilder builder)
    {
        //Production contexts
        // builder.AddNpgsqlDbContext<AccountContext>("todo-db");
        //Dev contexts
        builder.Services.AddDbContext<AccountContext>(options =>
        {
            options.UseExceptionProcessor();
            options.UseNpgsql();
        });
        return builder;
    }
}