namespace Todo.Shared.Models.Request;

public record struct NewTaskRequest(string Title, string Description)
{
    public string Title { get; init; } = Title;
    public string Description { get; init; } = Description;
}