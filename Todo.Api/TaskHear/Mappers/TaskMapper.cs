using Riok.Mapperly.Abstractions;

using Todo.Api.TaskHear.Models;

namespace Todo.Api.TaskHear.Mappers;
[Mapper(EnumMappingStrategy = EnumMappingStrategy.ByValueCheckDefined, EnumMappingIgnoreCase = true, EnabledConversions = MappingConversionType.All)]
public static partial class TaskMapper
{
    public static TaskEntity Map((EntityKey<Guid> CreatorKey, NewTaskRequest Request) model)
    {
        var currenTime = DateTimeOffset.UtcNow.DateTime;
        return new()
        {
            Title = model.Request.Title,
            Description = model.Request.Description,
            RegisterDate = DateOnly.FromDateTime(currenTime),
            RegisterTime = TimeOnly.FromDateTime(currenTime),
            CreatorKey = model.CreatorKey
        };
    }
    [MapPropertyFromSource(nameof(TaskViewResponse.Creation),Use = nameof(MapCreation))]
    public static partial TaskViewResponse ToRequest(this TaskEntity model);
    private static DateTime MapCreation(TaskEntity entity) => entity.RegisterDate.ToDateTime(entity.RegisterTime); 
}