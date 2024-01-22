using ApiTemplate.Application.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTemplate.Application.Services;

/// <summary>
/// Service for update a weather forecast.
/// </summary>
public interface IUpdateWeatherForecastService
{
    /// <summary>
    /// Update a weather forecast.
    /// </summary>
    /// <param name="weatherForecastId"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<GetWeatherForecastResponseContract> UpdateWeatherForecastAsync(Guid? weatherForecastId, UpdateWeatherForecastRequestContract request, CancellationToken cancellationToken);
}
