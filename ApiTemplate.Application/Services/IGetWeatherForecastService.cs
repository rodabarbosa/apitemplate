using ApiTemplate.Application.Contracts;
using System;
using System.Threading.Tasks;

namespace ApiTemplate.Application.Services;

/// <summary>
/// Service for get a weather forecast
/// </summary>
public interface IGetWeatherForecastService
{
    /// <summary>
    /// Get a weather forecast
    /// </summary>
    /// <param name="weatherForecastId"></param>
    /// <returns></returns>
    Task<GetWeatherForecastResponseContract> GetWeatherForecastAsync(Guid? weatherForecastId);
}
