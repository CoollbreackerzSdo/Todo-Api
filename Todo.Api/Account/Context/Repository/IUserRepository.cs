using Todo.Api.Account.Models;
using Todo.Api.Common.Context.Repository;

namespace Todo.Api.Account.Context.Repository;

public interface IUserRepository : IRepository<UserEntity>;