namespace Todo.Shared.Models.Response;

public record struct TaskViewResponse(string Title, string Description, DateTime Creation, string State)
{
    public string Title { get; init; } = Title;
    public string Description { get; init; } = Description;
    public DateTime Creation { get; init; } = Creation;
    public string State { get; init; } = State;
}