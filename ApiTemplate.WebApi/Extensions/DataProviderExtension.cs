using ApiTemplate.Application.Services;
using ApiTemplate.Application.Services.Authentication;
using ApiTemplate.Application.Services.WeatherForecasts;
using ApiTemplate.Domain.Repositories;
using ApiTemplate.Infra.Data;
using ApiTemplate.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.WebApi.Extensions;

/// <summary>
/// ServiceCollection Extensions
/// </summary>
static public class DataProviderExtension
{
    /// <summary>
    ///   Add Database configuration to services.
    /// </summary>
    /// <param name="services"></param>
    static public void AddDataProviders(this IServiceCollection services)
    {
        _ = services.AddDbContext<ApiTemplateContext>(o => o.UseInMemoryDatabase("ApiTemplate"));

        _ = services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
        _ = services.AddScoped<IAuthenticateService, AuthenticateService>();
        _ = services.AddScoped<ICreateWeatherForecastService, CreateWeatherForecastService>();
        _ = services.AddScoped<IDeleteWeatherForecastService, DeleteWeatherForecastService>();
        _ = services.AddScoped<IGetWeatherForecastService, GetWeatherForecastService>();
        _ = services.AddScoped<IGetAllWeatherForecastsService, GetAllWeatherForecastsService>();
        _ = services.AddScoped<IUpdateWeatherForecastService, UpdateWeatherForecastService>();

        _ = services.AddScoped<IUserRepository, UserRepository>();
    }
}
