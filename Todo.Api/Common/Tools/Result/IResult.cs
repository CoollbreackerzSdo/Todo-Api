namespace Todo.Api.Common.Tools.Result;

public interface IResult
{
    bool IsSuccess => Status is ResultStatus.Ok or ResultStatus.NoContent;
    ResultStatus Status { get; }
    string? Message { get; }
}
public enum ResultStatus
{
    Ok = 1,
    Error = 2,
    NoContent = 3
}