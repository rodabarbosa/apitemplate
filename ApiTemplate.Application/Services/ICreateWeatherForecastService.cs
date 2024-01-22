using ApiTemplate.Application.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTemplate.Application.Services;

/// <summary>
/// Service for create new weather forecast
/// </summary>
public interface ICreateWeatherForecastService
{
    /// <summary>
    /// Create new weather forecast
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<GetWeatherForecastResponseContract> CreateWeatherForecastAsync(CreateWeatherForecastRequestContract request, CancellationToken cancellationToken);
}
