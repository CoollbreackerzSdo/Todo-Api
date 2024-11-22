using Project;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Todo_Api>("api");

builder.Build().Run();