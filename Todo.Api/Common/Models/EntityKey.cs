namespace Todo.Api.Common.Models;

public record class EntityKey<T>(T Value)
    where T : notnull
{
    public override string ToString()
        => Value.ToString()!;
    public static implicit operator T(EntityKey<T> key) => key.Value;
    public static implicit operator EntityKey<T>(T value) => new(value);
}