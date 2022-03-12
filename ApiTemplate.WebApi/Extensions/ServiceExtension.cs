using System.Reflection;
using ApiTemplate.Application.Interfaces;
using ApiTemplate.Application.Services;
using ApiTemplate.Application.Validators;
using ApiTemplate.Domain.Interfaces;
using ApiTemplate.Infra.Data;
using ApiTemplate.Infra.Data.Repositories;
using ApiTemplate.WebApi.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ApiTemplate.WebApi.Extensions;

public static class ServiceExtension
{
    public static void Configure(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
            options.SuppressMapClientErrors = true;
        });

        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ExceptionFilter));
            options.RespectBrowserAcceptHeader = true;
        });

        services.AddResponseCompression();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        AddSwagger(services);

        AddFluentValidation(services);

        AddDataProviders(services);
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo {Title = "ApiTemplate", Version = "v1"});
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
    }

    private static void AddFluentValidation(IServiceCollection services)
    {
        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<WeatherForecastValidator>());
    }

    private static void AddDataProviders(IServiceCollection services)
    {
        services.AddDbContext<ApiTemplateContext>(o => o.UseInMemoryDatabase("ApiTemplate"));

        services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
        services.AddScoped<IWeatherForecastService, WeatherForecastService>();
    }
}
