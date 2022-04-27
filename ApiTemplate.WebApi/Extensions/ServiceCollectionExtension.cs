using System.Reflection;
using ApiTemplate.Application.Interfaces;
using ApiTemplate.Application.Jwt.Interfaces;
using ApiTemplate.Application.Jwt.Models;
using ApiTemplate.Application.Services;
using ApiTemplate.Domain.Interfaces;
using ApiTemplate.Infra.Data;
using ApiTemplate.Infra.Data.Repositories;
using ApiTemplate.WebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace ApiTemplate.WebApi.Extensions;

/// <summary>
/// ServiceCollection Extensions
/// </summary>
public static class ServiceCollectionExtension
{
//TODO: studying requirement to make a unit test for and how to

    /// <summary>
    /// Add JWT configuration to services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        var signingConfigurations = new SigningConfiguration();
        services.AddSingleton(typeof(ISigningConfiguration), signingConfigurations);

        var tokenConfigurations = new TokenConfiguration();
        new ConfigureFromConfigurationOptions<TokenConfiguration>(configuration.GetSection("TokenConfiguration"))
            .Configure(tokenConfigurations);
        services.AddSingleton(typeof(ITokenConfiguration), tokenConfigurations);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Events.OnRedirectToAccessDenied =
                    options.Events.OnRedirectToLogin = c =>
                    {
                        c.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.FromResult<object>(null!);
                    };
            })
            .AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = true;
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;

                var paramsValidation = options.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

        services.AddAuthorization(options =>
        {
            options.InvokeHandlersAfterFailure = true;
            options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
        });
    }

    /// <summary>
    ///    Add swagger configuration to services.
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddApiVersioning();

        services.AddSwaggerGen(options =>
        {
            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in provider.ApiVersionDescriptions)
                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = $"ApiTemplate {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                    Description = "ApiTemplate",
                    TermsOfService = default,
                    Contact = new OpenApiContact
                    {
                        Email = default,
                        Name = "Rodrigo A. Barbosa",
                        Url = default
                    },
                    License = default
                });

            options.OperationFilter<SwaggerFilter>();
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetEntryAssembly()!.GetName().Name}.xml"));

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Description = "Authorization token using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "bearer",
                Type = SecuritySchemeType.Http
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    /// <summary>
    ///   Add Database configuration to services.
    /// </summary>
    /// <param name="services"></param>
    public static void AddDataProviders(this IServiceCollection services)
    {
        services.AddDbContext<ApiTemplateContext>(o => o.UseInMemoryDatabase("ApiTemplate"));

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
        services.AddScoped<IWeatherForecastService, WeatherForecastService>();

        services.AddScoped<IUserRepository, UserRepository>();
    }

    /// <summary>
    /// Configure error handling.
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureDefaultErrorHandler(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
            options.SuppressMapClientErrors = true;
        });
    }
}
