using System.Collections.Immutable;
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
using Todo.Api.Common.Middlewares;
using Todo.Api.TaskHear.Context;
using Todo.Api.TaskHear.Context.Repository;
using Todo.Api.TaskHear.Endpoints;
using Todo.Api.TaskHear.Handlers.Create;
using Todo.Api.TaskHear.Handlers.Read;
using Todo.Api.TaskHear.Validators;

namespace Todo.Api;

public static class ServiceDiscovery
{
    public static IHostApplicationBuilder AddDbContexts(this IHostApplicationBuilder builder)
    {
        // Production contexts
        builder.AddNpgsqlDbContext<AccountContext>("todo-db", null, options =>
        {
            options.UseExceptionProcessor();
            options.UseNpgsql();
        });
        builder.AddNpgsqlDbContext<TaskContext>("todo-db", null, options =>
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
        // builder.Services.AddDbContext<TaskContext>(options =>
        // {
        //     options.UseExceptionProcessor();
        //     options.UseNpgsql();
        // });
        return builder;
    }
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddSingleton<IValidator<NewTaskRequest>, NewTaskRequestValidator>();
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
        services.AddTransient<IHandlerAsync<(EntityKey<Guid> CreatorKey, NewTaskRequest Request), TaskViewResponse>, CreateTaskHandler>();
        services.AddTransient<IResponseHandler<ImmutableArray<TaskViewResponse>>, ReadTaskHandler>();
        services.AddTransient<IHandler<Guid, TaskViewResponse>, ReadTaskHandler>();
        services.AddTransient<IHandler<Guid, ImmutableArray<TaskViewResponse>>, ReadTaskHandler>();
        return services;
    }
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ITaskRepository, TaskRepository>();
        return services;
    }
    public static IServiceCollection AddMiddlewares(this IServiceCollection service)
    {
        service.AddTransient<ErrorMiddleware>();
        return service;
    }
    public static void MapMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ErrorMiddleware>();
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
        builder.MapManagerTaskEndpoints();
        return builder;
    }
}