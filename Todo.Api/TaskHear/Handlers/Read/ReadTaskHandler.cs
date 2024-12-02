
using System.Collections.Immutable;

using Todo.Api.TaskHear.Context.Repository;
using Todo.Api.TaskHear.Models;

namespace Todo.Api.TaskHear.Handlers.Read;

public sealed class ReadTaskHandler(ITaskRepository repository) : IHandler<Guid, ImmutableArray<TaskViewResponse>>, IResponseHandler<ImmutableArray<TaskViewResponse>>, IHandler<Guid, TaskViewResponse>
{
    public Result<TaskViewResponse> Handle(Guid request)
        => repository.Find(request) is TaskEntity model ? new TaskViewResponse(model.Title, model.Description, model.RegisterDate.ToDateTime(model.RegisterTime), model.State.ToString()) : Result.NoContent();
    public Result<ImmutableArray<TaskViewResponse>> Handle()
        => repository.GetAll().Select(model => new TaskViewResponse(model.Title, model.Description, model.RegisterDate.ToDateTime(model.RegisterTime), model.State.ToString())).ToImmutableArray();
    Result<ImmutableArray<TaskViewResponse>> IHandler<Guid, ImmutableArray<TaskViewResponse>>.Handle(Guid request)
        => repository.GetAll().Where(x => x.CreatorKey.Value == request).Select(model => new TaskViewResponse(model.Title, model.Description, model.RegisterDate.ToDateTime(model.RegisterTime), model.State.ToString())).ToImmutableArray();
}