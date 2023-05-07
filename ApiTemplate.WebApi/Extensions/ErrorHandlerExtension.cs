using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.WebApi.Extensions;

/// <summary>
/// ServiceCollection Extensions
/// </summary>
static public class ErrorHandlerExtension
{
    /// <summary>
    /// Configure error handling.
    /// </summary>
    /// <param name="services"></param>
    static public void ConfigureDefaultErrorHandler(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
            options.SuppressMapClientErrors = true;
        });
    }
}
