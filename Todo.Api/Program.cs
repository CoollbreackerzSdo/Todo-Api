var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddDbContexts();
builder.Services.AddOpenApi();
builder.Services.AddRepositories();
builder.Services.AddHandlers();
builder.Services.AddValidators();
builder.Services.AddAuthServices();
builder.Services.AddHahsServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapMigrations();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultEndpoints();
app.MapEndpoints();

app.Run();