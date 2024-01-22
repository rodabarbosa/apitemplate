using ApiTemplate.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTemplate.Domain.Repositories;

public interface IWeatherForecastRepository
{
    IQueryable<WeatherForecast> Get(Expression<Func<WeatherForecast, bool>> predicate = null!);
    WeatherForecast? Get(Guid id);
    Task<WeatherForecast?> GetAsync(Guid id, CancellationToken cancellationToken);
    void Add(WeatherForecast weatherForecast);
    Task AddAsync(WeatherForecast weatherForecast, CancellationToken cancellationToken);
    void Update(WeatherForecast weatherForecast);
    Task UpdateAsync(WeatherForecast weatherForecast, CancellationToken cancellationToken);
    void Delete(Guid id);
    void Delete(WeatherForecast weatherForecast);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAsync(WeatherForecast? weatherForecast, CancellationToken cancellationToken);
}
