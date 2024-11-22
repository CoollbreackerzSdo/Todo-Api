using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("todo-db")
    .WithVolume("Todo", "/var/lib/postgres")
    .WithImage("postgres", "17.0-alpine3.20")
    .WithLifetime(ContainerLifetime.Persistent);

builder.AddProject<Todo_Api>("api")
    .WithReference(postgres)
    .WaitFor(postgres);

builder.Build().Run();