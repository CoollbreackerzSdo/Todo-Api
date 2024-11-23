using System.Security.Claims;

using Todo.Api.Account.Context.Repository;
using Todo.Api.Account.Mappers;

namespace Todo.Api.Account.Handlers.Create;

public class SigUpHandler(IUserRepository repository) : IHandlerAsync<SignUpRequest, IEnumerable<Claim>>
{
    public async Task<Result<IEnumerable<Claim>>> Handle(SignUpRequest request, CancellationToken token = default)
    {
        var model = UserMapper.Map(request);
        repository.Add(model);
        var saveResult = await repository.SaveChangesAsync(token);
        return saveResult.IsSuccess ? Result.Success<IEnumerable<Claim>>([
            new Claim(ClaimTypes.NameIdentifier,model.Security.UserName),
            new Claim("id",model.Key.ToString())
        ]) : saveResult;
    }
}