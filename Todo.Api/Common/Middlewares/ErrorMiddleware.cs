
using Microsoft.AspNetCore.Mvc;

namespace Todo.Api.Common.Middlewares;

public sealed class ErrorMiddleware(ILogger<ErrorMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (BadHttpRequestException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new ProblemDetails()
            {
                Title = "Invalid Json Format",
                Detail = "Invalid JSON formatting please check the body of the content",
                Status = StatusCodes.Status400BadRequest
            });
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            context.Response.StatusCode = StatusCodes.Status418ImATeapot;
            await context.Response.WriteAsJsonAsync(new ProblemDetails()
            {
                Title = "Server Error",
                Detail = "There was an error on the server because it is a teapot please order a cup again later",
                Status = StatusCodes.Status418ImATeapot
            });
        }
    }
}