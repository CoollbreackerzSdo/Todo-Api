using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Todo.Api.Common.Context.Config;

public abstract class ConfigurationBase<T> : IEntityTypeConfiguration<T>
    where T : EntityBase
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.Key)
            .HasConversion(x => x.Value, x => x);

        builder.HasKey(x => x.Key);
    }
}