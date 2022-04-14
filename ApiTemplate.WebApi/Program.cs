using ApiTemplate.WebApi.Extensions;
using ApiTemplate.WebApi.Filters;
using ApiTemplate.WebApi.Middlewares;
using Swashbuckle.AspNetCore.SwaggerUI;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureDefaultErrorHandler();

builder.Services.AddResponseCompression();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(CustomExceptionFilterAttribute));
    options.RespectBrowserAcceptHeader = true;
});

builder.Services.AddJwtService(builder.Configuration);

builder.Services.ConfigureSwagger();

builder.Services.AddDataProviders();

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
_ = app.UseSwagger();
_ = app.UseSwaggerUI(c => c.DocExpansion(DocExpansion.None));

app.UseMiddleware<RequestHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseResponseCompression();

app.UseAuthorization();

app.MapControllers();

app.Run();