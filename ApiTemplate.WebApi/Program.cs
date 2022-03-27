using ApiTemplate.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.Configure();

// Add services to the container.
builder.Services.Configure(builder.Configuration);

var app = builder.Build();

app.Configure();

app.Run();
