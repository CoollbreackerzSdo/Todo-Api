using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Todo.Api.TaskHear.Models;

namespace Todo.Api.TaskHear.Context.Config;

public sealed class TaskConfiguration : ConfigurationBase<TaskEntity>
{
    public override void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Title)
            .HasColumnName("title")
            .HasMaxLength(50);

        builder.Property(x => x.CreatorKey)
            .HasColumnName("creator_key")
            .HasConversion(x => x.Value, x => x);

        builder
            .HasIndex(x => x.CreatorKey);

        builder.Property(x => x.State)
            .HasColumnName("state");

        builder.Property(x => x.Description)
            .HasMaxLength(200)
            .HasColumnName("description");

        builder.Property(x => x.RegisterDate)
            .HasColumnName("register_date");

        builder.Property(x => x.RegisterTime)
            .HasColumnName("register_time");
    }
}