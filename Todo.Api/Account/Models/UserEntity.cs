
namespace Todo.Api.Account.Models;

public sealed class UserEntity : EntityBase, IAccount, IRegister
{
    public required SecurityAccount Security { get; init; }
    public DetailAccount? Detail { get; set; }
    public required DateOnly RegisterDate { get; init; }
}