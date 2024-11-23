using System.Security.Claims;

using Microsoft.AspNetCore.Identity;

using Todo.Api.Account.Context.Repository;
using Todo.Api.Account.Models;

namespace Todo.Api.Account.Handlers.Create;

public sealed class SigInHandler(IUserRepository repository, IPasswordHasher<UserEntity> hasher) : IHandlerAsync<SignInRequest, IEnumerable<Claim>>
{
    public Task<Result<IEnumerable<Claim>>> Handle(SignInRequest request, CancellationToken token = default)
    {
        var model = repository.SingleOrDefault(x => x.Security.UserName == request.UserName);
        if (model is null) return Task.FromResult<Result<IEnumerable<Claim>>>(Result.NoContent());
        else if (hasher.VerifyHashedPassword(model, model.Security.Password, request.Password) == PasswordVerificationResult.Success)
            return Task.FromResult(Result.Success<IEnumerable<Claim>>([
            new Claim(ClaimTypes.NameIdentifier,model.Security.UserName),
            new Claim("id",model.Key.ToString())
        ]));
        return Task.FromResult<Result<IEnumerable<Claim>>>(Result.Invalid());
    }
}