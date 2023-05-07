using ApiTemplate.Application.Jwt.Models;
using ApiTemplate.WebApi.Configs;
using ApiTemplate.WebApi.Extensions;
using ApiTemplate.WebApi.Filters;
using ApiTemplate.WebApi.Middlewares;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureDefaultErrorHandler();

builder.Services.AddResponseCompression();

builder.Services.AddControllers(options =>
    {
        options.Filters.Add(typeof(CustomExceptionFilterAttribute));
        options.RespectBrowserAcceptHeader = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.WriteIndented = false;
    });

var configuration = builder.Configuration;

var tokenConfigurations = new TokenConfiguration();
new ConfigureFromConfigurationOptions<TokenConfiguration>(configuration.GetSection("TokenConfiguration"))
    .Configure(tokenConfigurations);

builder.Services.AddJwtService(tokenConfigurations);

var apiDescriptionConfig = new ApiDescriptionConfig();
new ConfigureFromConfigurationOptions<ApiDescriptionConfig>(configuration.GetSection("ApiDescription"))
    .Configure(apiDescriptionConfig);

builder.Services.ConfigureSwagger(apiDescriptionConfig);

builder.Services.AddDataProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
_ = app.UseSwagger();
_ = app.UseSwaggerUI(c => c.DocExpansion(DocExpansion.None));

app.UseMiddleware<RequestHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseResponseCompression();

app.UseAuthorization();

app.MapControllers();

app.Run();
