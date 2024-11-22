
namespace Todo.Api.Account.Models;

public interface IAccount
{
    SecurityAccount Security { get; }
    DetailSecurity? Detail { get; }
}