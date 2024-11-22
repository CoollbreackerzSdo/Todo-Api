using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("todo-db")
    .WithVolume("Todo", "/var/lib/postgres")
    .WithImage("postgres", "17.0-alpine3.20")
    .WithLifetime(ContainerLifetime.Persistent);

var api = builder.AddProject<Todo_Api>("api")
    .WithReference(postgres)
    .WaitFor(postgres);

builder.AddProject<Todo>("client")
    .WithReference(api)
    .WaitFor(api);

builder.Build().Run();