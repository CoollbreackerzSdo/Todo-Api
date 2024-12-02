using Scalar.AspNetCore;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, logger) =>
{
    logger.WriteTo.Console();
});
builder.AddServiceDefaults();
builder.AddDbContexts();
builder.Services.AddOpenApi();
builder.Services.AddRepositories();
builder.Services.AddHandlers();
builder.Services.AddValidators();
builder.Services.AddAuthServices();
builder.Services.AddHahsServices();
builder.Services.AddMiddlewares();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapMigrations();
    app.MapScalarApiReference(options =>
        options.WithTheme(ScalarTheme.Moon)
        .AddServer("https://localhost:3011")
        .AddServer("http://localhost:3010"));
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapMiddlewares();
app.MapDefaultEndpoints();
app.MapEndpoints();

app.Run();