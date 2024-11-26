using Todo.Api.Account.Context.Config;
using Todo.Api.Account.Models;

namespace Todo.Api.Account.Context;

public sealed class AccountContext(DbContextOptions<AccountContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; init; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}