namespace Todo.Shared.Models.Request;

public record struct SignInRequest(string UserName, string Password)
{
    public string UserName { get; init; } = UserName;
    public string Password { get; init; } = Password;
}