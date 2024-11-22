namespace Todo.Api.Common.Handlers;

public interface IHandler<TRequest, TResponse>
{
    Result<TResponse> Handle(TRequest request);
}
public interface IHandlerAsync<TRequest, TResponse>
{
    Task<Result<TResponse>> Handle(TRequest request, CancellationToken token = default);
}
public interface IRequestHandler<TRequest>
{
    Result Handle(TRequest request);
}
public interface IRequestHandlerAsync<TRequest>
{
    Result Handle(TRequest request, CancellationToken token = default);
}
public interface IResponseHandler<TResponse>
{
    TResponse Handle();
}
public interface IHandlerAsync<TResponse>
{
    Task<TResponse> Handle(CancellationToken token = default);
}