using Microsoft.AspNetCore.Components;

namespace Todo.Components.Pages.Auth;

public partial class SignIn
{
    private Task Send()
    {
        var client = HttpFactory.CreateClient("server");
        
        return Task.CompletedTask;
    }
    [Inject]
    internal IHttpClientFactory HttpFactory { get; set; } = null!;
}