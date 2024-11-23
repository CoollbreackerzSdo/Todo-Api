using System.Security.Claims;

using Microsoft.AspNetCore.Identity;

using Todo.Api.Account.Context.Repository;
using Todo.Api.Account.Mappers;
using Todo.Api.Account.Models;

namespace Todo.Api.Account.Handlers.Create;

public sealed class SigUpHandler(IUserRepository repository, IPasswordHasher<UserEntity> hasher) : IHandlerAsync<SignUpRequest, IEnumerable<Claim>>
{
    public async Task<Result<IEnumerable<Claim>>> Handle(SignUpRequest request, CancellationToken token = default)
    {
        var model = UserMapper.Map(request);
        model.Security.Password = hasher.HashPassword(model, model.Security.Password);
        repository.Add(model);
        var saveResult = await repository.SaveChangesAsync(token);
        return saveResult.IsSuccess ? Result.Success<IEnumerable<Claim>>([
            new Claim(ClaimTypes.NameIdentifier,model.Security.UserName),
            new Claim("id",model.Key.ToString())
        ]) : saveResult;
    }
}