using Todo.Api.Account.Models;
using Todo.Api.Common.Context.Repository;

namespace Todo.Api.Account.Context.Repository;

public sealed class UserRepository(AccountContext context) : GenericRepository<UserEntity, AccountContext>(context), IUserRepository
{
    
}