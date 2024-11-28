using Todo.Api.Common.Context.Repository;
using Todo.Api.TaskHear.Models;

namespace Todo.Api.TaskHear.Context.Repository;

public sealed class TaskRepository(TaskContext context) : GenericRepository<TaskEntity, TaskContext>(context), ITaskRepository
{
}