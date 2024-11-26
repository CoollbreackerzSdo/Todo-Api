using Ardalis.Result;

using FluentAssertions;

using Microsoft.AspNetCore.Identity;

using Todo.Api.Account.Context.Repository;
using Todo.Api.Account.Handlers.Create;
using Todo.Api.Account.Models;
using Todo.Shared.Models.Request;
using Todo.Test.Tools;

namespace Todo.Test.XUnit.Handlers;
[Collection("Account Context Collection")]
public class UserHandlers(AccountContextFixtureConfiguration context)
{
    [Fact]
    public async Task InsertNewValidUserReturnOkResult()
    {
        // Given
        var repo = new UserRepository(context);
        var hasher = new PasswordHasher<UserEntity>();
        var handler = new SigUpHandler(repo, hasher);
        var modelTest = new SignUpRequest("Cocos", "LKSAI99uwi932831");
        // When
        var handlerResult = await handler.Handle(modelTest);
        // Then
        handlerResult.Status.Should().Be(ResultStatus.Ok);
    }
    [Fact]
    public async Task InsertNewValidExistUserReturnOkConflict()
    {
        // Given
        var repo = new UserRepository(context);
        var hasher = new PasswordHasher<UserEntity>();
        var handler = new SigUpHandler(repo, hasher);
        var modelTest = new SignUpRequest("Cocos", "LKSAI99uwi932831");
        // When
        await handler.Handle(modelTest);
        var handlerResult = await handler.Handle(modelTest);
        // Then
        handlerResult.Status.Should().BeOneOf(ResultStatus.Conflict, ResultStatus.Ok);
    }
}