
using System.Security.Claims;

using Microsoft.AspNetCore.Http.HttpResults;

using Todo.Api.Common.Auth.Providers;
using Todo.Api.Common.Endpoint.Filters;

namespace Todo.Api.Account.Endpoints;

public static class AuthEndpoint
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder builder)
    {
        var endpoint = builder.MapGroup("auth")
            .WithTags("Auth");

        endpoint.MapPost("sign-up", SignUp)
            .Accepts<SignUpRequest>("application/json")
            .AddEndpointFilter<GenericValidatorFilter<SignUpRequest>>()
            .Produces(StatusCodes.Status200OK)
            .Produces<string>(StatusCodes.Status409Conflict)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithOpenApi();

        endpoint.MapGet("sign-out", SigOut)
            .RequireAuthorization(Policies.Global)
            .Produces(StatusCodes.Status200OK)
            .WithOpenApi();

        endpoint.MapPost("sign-in", SignIn)
            .Accepts<SignInRequest>("application/json")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithOpenApi();

        return endpoint;
    }

    public static async Task<Results<SignInHttpResult, NotFound, BadRequest>> SignIn(SignInRequest request, IHandlerAsync<SignInRequest, IEnumerable<Claim>> handler, CancellationToken token)
    {
        var handlerResult = await handler.Handle(request, token);
        return handlerResult.Status switch
        {
            ResultStatus.Ok => TypedResults.SignIn(
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        handlerResult.Value.Append(new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddHours(10).ToString())),
                        authenticationType: Schemes.Default
                    )
                ), authenticationScheme: Schemes.Default
            ),
            ResultStatus.Invalid => TypedResults.BadRequest(),
            _ => TypedResults.NotFound()
        };
    }

    public static SignOutHttpResult SigOut()
        => TypedResults.SignOut();

    public static async Task<Results<SignInHttpResult, NotFound, Conflict<string>>> SignUp(SignUpRequest request, IHandlerAsync<SignUpRequest, IEnumerable<Claim>> handler, CancellationToken token)
    {
        var handlerResult = await handler.Handle(request, token);
        return handlerResult.Status switch
        {
            ResultStatus.Ok => TypedResults.SignIn(
                new ClaimsPrincipal(
                    new ClaimsIdentity(
                        handlerResult.Value.Append(new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddHours(10).ToString())),
                        authenticationType: Schemes.Default
                    )
                ), authenticationScheme: Schemes.Default
            ),
            ResultStatus.Conflict => TypedResults.Conflict(string.Concat(handlerResult.Errors)),
            _ => TypedResults.NotFound()
        };
    }
}