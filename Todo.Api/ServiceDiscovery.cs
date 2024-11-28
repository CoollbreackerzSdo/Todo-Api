using System.Security.Claims;

using EntityFramework.Exceptions.PostgreSQL;

using FluentValidation;

using Microsoft.AspNetCore.Identity;

using Todo.Api.Account.Context;
using Todo.Api.Account.Context.Repository;
using Todo.Api.Account.Endpoints;
using Todo.Api.Account.Handlers.Create;
using Todo.Api.Account.Models;
using Todo.Api.Account.Validators;
using Todo.Api.Common.Auth.Providers;
using Todo.Api.TaskHear.Context;

namespace Todo.Api;

public static class ServiceDiscovery
{
    public static IHostApplicationBuilder AddDbContexts(this IHostApplicationBuilder builder)
    {
        //Production contexts
        // builder.AddNpgsqlDbContext<AccountContext>("todo-db", null, options =>
        // {
        //     options.UseExceptionProcessor();
        //     options.UseNpgsql();
        // });
        // builder.AddNpgsqlDbContext<TaskContext>("todo-db", null, options =>
        // {
        //     options.UseExceptionProcessor();
        //     options.UseNpgsql();
        // });
        //Dev contexts
        builder.Services.AddDbContext<AccountContext>(options =>
        {
            options.UseExceptionProcessor();
            options.UseNpgsql();
        });
        builder.Services.AddDbContext<TaskContext>(options =>
        {
            options.UseExceptionProcessor();
            options.UseNpgsql();
        });
        return builder;
    }
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddSingleton<IValidator<SignUpRequest>, SignUpValidator>();
        return services;
    }
    public static IServiceCollection AddHahsServices(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
        return services;
    }
    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        services.AddAuthentication(Schemes.Default)
            .AddCookie(Schemes.Default, options =>
            {

            });
        services.AddAuthorizationBuilder()
            .AddDefaultPolicy(Policies.Global, options => options.AddAuthenticationSchemes([Schemes.Default]).RequireAuthenticatedUser());
        return services;
    }
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddTransient<IHandlerAsync<SignInRequest, IEnumerable<Claim>>, SigInHandler>();
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
        using var accountContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<AccountContext>();
        accountContext.Database.Migrate();
        using var taskContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<TaskContext>();
        taskContext.Database.Migrate();
    }
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapAuthEndpoints();
        return builder;
    }
}