using Api;
using Server;

var builder = WebApplication.CreateBuilder(args);

builder.AddProject();

var app = builder.Build();

app.UseProjectApi();

app.Run();