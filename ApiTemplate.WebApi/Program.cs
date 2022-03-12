using ApiTemplate.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure();

var app = builder.Build();

app.Configure();

app.Run();
