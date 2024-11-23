
namespace Todo.Api.Account.Models;

public interface IAccount
{
    SecurityAccount Security { get; }
    DetailAccount? Detail { get; }
}