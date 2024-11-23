var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddDbContexts();
builder.Services.AddOpenApi();
builder.Services.AddRepositories();
builder.Services.AddHandlers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapMigrations();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapDefaultEndpoints();

app.Run();