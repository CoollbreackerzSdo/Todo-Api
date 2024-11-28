
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

        return endpoint;
    }

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