
using System.Collections.Immutable;
using System.Security.Claims;

using Microsoft.AspNetCore.Http.HttpResults;

using Todo.Api.Common.Auth.Providers;
using Todo.Api.Common.Endpoint.Filters;

namespace Todo.Api.TaskHear.Endpoints;

public static class ManagerEndpoint
{
    public static IEndpointRouteBuilder MapManagerTaskEndpoints(this IEndpointRouteBuilder builder)
    {
        var endpoint = builder.MapGroup("task")
            .WithTags(["Task"])
            .RequireAuthorization([Policies.Global]);

        endpoint.MapPost("", CreateNewTask)
            .AddEndpointFilter<GenericValidatorFilter<NewTaskRequest>>()
            .Accepts<NewTaskRequest>("application/json")
            .Produces<TaskViewResponse>()
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi();

        endpoint.MapGet("", GetAll)
            .Produces<ImmutableArray<TaskViewResponse>>()
            .WithOpenApi();

        endpoint.MapGet("id", GetById)
            .Produces<TaskViewResponse>()
            .ProducesProblem(StatusCodes.Status204NoContent)
            .WithOpenApi();

        return endpoint;
    }

    private static Results<Ok<TaskViewResponse>, NoContent, NotFound> GetById(Guid id, IHandler<Guid, TaskViewResponse> handler)
    {
        var handlerResult = handler.Handle(id);
        return handlerResult.Status switch
        {
            ResultStatus.Ok => TypedResults.Ok(handlerResult.Value),
            ResultStatus.NoContent => TypedResults.NoContent(),
            _ => TypedResults.NotFound()
        };
    }

    private static Ok<ImmutableArray<TaskViewResponse>> GetAll(ClaimsPrincipal claims, IHandler<Guid, ImmutableArray<TaskViewResponse>> handler)
        => TypedResults.Ok(handler.Handle(Guid.Parse(claims.FindFirstValue("id")!)).Value);
    private static async Task<Results<Ok<TaskViewResponse>, NotFound>> CreateNewTask(NewTaskRequest request, ClaimsPrincipal claims, IHandlerAsync<(EntityKey<Guid> CreatorKey, NewTaskRequest Request), TaskViewResponse> handler, CancellationToken token)
    {
        var handlerResult = await handler.Handle((Guid.Parse(claims.FindFirstValue("id")!), request), token);
        return handlerResult.Status switch
        {
            ResultStatus.Ok => TypedResults.Ok(handlerResult.Value),
            _ => TypedResults.NotFound()
        };
    }
}