
using Todo.Api.TaskHear.Context.Repository;
using Todo.Api.TaskHear.Mappers;

namespace Todo.Api.TaskHear.Handlers.Create;

public sealed class CreateTaskHandler(ITaskRepository repository) : IHandlerAsync<(EntityKey<Guid> CreatorKey, NewTaskRequest Request), TaskViewResponse>
{
    public async Task<Result<TaskViewResponse>> Handle((EntityKey<Guid> CreatorKey, NewTaskRequest Request) request, CancellationToken token = default)
    {
        var model = TaskMapper.Map(request);
        repository.Add(model);
        var saveResult = await repository.SaveChangesAsync(token);
        return saveResult.IsSuccess ? model.ToRequest() : saveResult;
    }
}