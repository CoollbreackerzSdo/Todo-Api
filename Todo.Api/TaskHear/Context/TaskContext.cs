using Todo.Api.TaskHear.Context.Config;
using Todo.Api.TaskHear.Models;

namespace Todo.Api.TaskHear.Context;

public sealed class TaskContext(DbContextOptions<TaskContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
    }
    public DbSet<TaskEntity> Tasks { get; init; } = null!;
}