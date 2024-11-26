using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Todo.Api.Account.Models;

namespace Todo.Api.Account.Context.Config;

public sealed class UserConfiguration : ConfigurationBase<UserEntity>
{
    public override void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.RegisterDate)
            .HasColumnName("register_date");

        builder.OwnsOne(x => x.Detail, options =>
        {
            options.Property(x => x.FirstName)
                .HasColumnName("first_name")
                .HasColumnType("varchar")
                .HasMaxLength(30);

            options.Property(x => x.LastName)
                .HasColumnName("last_name")
                .HasColumnType("varchar")
                .HasMaxLength(30);
        });

        builder.OwnsOne(x => x.Security, options =>
        {
            options.HasIndex(x => x.UserName)
                .IsUnique();

            options.Property(x => x.UserName)
                .HasColumnName("user_name")
                .HasColumnType("varchar")
                .HasMaxLength(50);

            options.Property(x => x.Password)
                .HasColumnName("password")
                .HasColumnType("text");
        });
    }
}