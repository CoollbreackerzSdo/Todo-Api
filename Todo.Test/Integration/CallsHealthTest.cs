using Aspire.Hosting;

namespace Todo.Test.Integration;

public class CallsHealthTest : IAsyncLifetime
{
    [Fact(Timeout = 100)]
    public async Task GetApiHealthStatusReturnsOkStatusCode()
    {
        // Arrange
        var resourceNotificationService = _app.Services.GetRequiredService<ResourceNotificationService>();
        // Act
        var httpClient = _app!.CreateHttpClient("api");
        await resourceNotificationService.WaitForResourceAsync("api", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));
        var response = await httpClient.GetAsync("/health");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    public async Task DisposeAsync() => await _app!.DisposeAsync();
    public async Task InitializeAsync()
    {
        var appHost = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.Todo_AppHost>();

        _app = await appHost.BuildAsync();

        await _app!.StartAsync();
    }
    private DistributedApplication _app = null!;
}