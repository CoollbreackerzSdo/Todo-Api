
namespace Todo.Api.TaskHear.Models;

public interface ITask : IRegister
{
    string Title { get; }
    string Description { get; }
    TimeOnly RegisterTime { get; }
    TaskState State { get; }
}
