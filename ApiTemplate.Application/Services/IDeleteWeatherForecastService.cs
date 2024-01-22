using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTemplate.Application.Services;

/// <summary>
/// Service for delete a weather forecast.
/// </summary>
public interface IDeleteWeatherForecastService
{
    /// <summary>
    /// Delete a weather forecast.
    /// </summary>
    /// <param name="weatherForecastId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteWeatherForecastAsync(Guid? weatherForecastId, CancellationToken cancellationToken);
}
