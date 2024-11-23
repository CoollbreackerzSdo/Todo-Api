
using FluentValidation;

namespace Todo.Api.Common.Endpoint.Filters;

public sealed class GenericValidatorFilter<T>(IValidator<T> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var validationResult = await validator.ValidateAsync(context.GetArgument<T>(0));
        return validationResult.IsValid ? await next(context) : TypedResults.ValidationProblem(validationResult.ToDictionary());
    }
}