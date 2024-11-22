namespace Todo.Shared.Models.Request;

public record struct SignUpRequest(string UserName, string Password, string? FirstName = null, string? LastName = null)
{
    public string UserName { get; init; } = UserName;
    public string Password { get; init; } = Password;
    public string? FirstName { get; init; } = FirstName;
    public string? LastName { get; init; } = LastName;
}