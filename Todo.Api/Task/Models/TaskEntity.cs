
namespace Todo.Api.Task.Models;

public class TaskEntity : EntityBase, ITask
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public TaskState State { get; set; } = TaskState.Process;
    public required DateOnly RegisterDate { get; init; }
    public required TimeOnly RegisterTime { get; init; }
}