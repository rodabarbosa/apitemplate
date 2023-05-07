using ApiTemplate.Application.Jwt.Interfaces;
using ApiTemplate.Application.Jwt.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ApiTemplate.WebApi.Extensions;

/// <summary>
/// ServiceCollection Extensions
/// </summary>
static public class JwtConfigureExtension
{
    /// <summary>
    /// Add JWT configuration to services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="tokenConfigurations"></param>
    static public void AddJwtService(this IServiceCollection services, TokenConfiguration tokenConfigurations)
    {
        var signingConfigurations = new SigningConfiguration();
        _ = services.AddSingleton(typeof(ISigningConfiguration), signingConfigurations);

        _ = services.AddSingleton(typeof(ITokenConfiguration), tokenConfigurations);

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

        _ = services.AddAuthorization(options =>
        {
            options.InvokeHandlersAfterFailure = true;
            options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
        });
    }
}
