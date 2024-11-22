namespace Todo.Api.Common.Models;

public class EntityBase : IEntity<Guid>
{
    public EntityKey<Guid> Key { get; init; } = Guid.CreateVersion7();
}