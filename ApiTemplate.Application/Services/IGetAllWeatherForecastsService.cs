using ApiTemplate.Application.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTemplate.Application.Services;

/// <summary>
/// Service for query weather forecast
/// </summary>
public interface IGetAllWeatherForecastsService
{
    /// <summary>
    /// Query all weather forecast
    /// </summary>
    /// <param name="param"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<GetWeatherForecastResponseContract>> GetAllWeatherForecastsAsync(string? param, CancellationToken cancellationToken);
}
