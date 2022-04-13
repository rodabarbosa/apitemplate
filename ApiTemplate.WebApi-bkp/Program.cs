using ApiTemplate.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

var logger = builder.Logging.Configure();
builder.Services.AddSingleton(logger);

// Add services to the container.
builder.Services.Configure(builder.Configuration);

var app = builder.Build();

app.Configure();

app.Run();
