namespace Todo.Api.Common.Tools.Result;

public class Result<T>(T? value) : IResult
{
    protected internal Result(T? value, string successMessage) : this(value) => Message = successMessage;
    protected Result(ResultStatus status, string? message = null) : this(default(T)) => (Status, Message) = (status, message);
    public static Result<T> Success(T value) => new(value);
    public static Result<T> Success(T value, string successMessage) => new(value, successMessage);
    public static Result<T> NoContent() => new(ResultStatus.NoContent);
    public static Result<T> NoContent(string errorMessage) => new(ResultStatus.NoContent, errorMessage);
    public static Result<T> Error() => new(ResultStatus.Error);
    public static Result<T> Error(string errorMessage) => new(ResultStatus.Error, errorMessage);
    public T? Value { get; init; } = value;
    public ResultStatus Status { get; protected init; } = ResultStatus.Ok;
    public string? Message { get; protected init; }
    public static implicit operator T(Result<T> result) => result.Value!;
    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(Result result) => new(default)
    {
        Status = result.Status,
        Message = result.Message,
    };
}
public class Result : Result<Result>
{
    protected internal Result(Result? value, string successMessage) : base(value, successMessage) { }
    protected internal Result(ResultStatus status) : base(status) { }
    protected internal Result(ResultStatus status, string message) : base(status, message) { }
    public Result Success() => new(default);
    public Result Success(string successMessage) => new(default(Result), successMessage);
    public Result NotContent() => new(ResultStatus.NoContent);
    public Result NotContent(string errorMessage) => new(ResultStatus.NoContent, errorMessage);
}