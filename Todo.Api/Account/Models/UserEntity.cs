
namespace Todo.Api.Account.Models;

public class UserEntity : EntityBase, IAccount, IRegister
{
    public required SecurityAccount Security { get; init; }
    public DetailSecurity? Detail { get; set; }
    public required DateOnly RegisterDate { get; init; }
}