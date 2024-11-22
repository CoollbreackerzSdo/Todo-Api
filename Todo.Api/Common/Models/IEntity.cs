namespace Todo.Api.Common.Models;

public interface IEntity<TKey>
    where TKey : notnull
{
    EntityKey<TKey> Key { get; }
}