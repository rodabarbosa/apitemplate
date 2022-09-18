using ApiTemplate.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiTemplate.Domain.Repositories;

public interface IWeatherForecastRepository
{
    IQueryable<WeatherForecast> Get(Expression<Func<WeatherForecast, bool>> predicate = null!);
    WeatherForecast? Get(Guid id);
    Task<WeatherForecast?> GetAsync(Guid id);
    void Add(WeatherForecast weatherForecast);
    Task AddAsync(WeatherForecast weatherForecast);
    void Update(WeatherForecast weatherForecast);
    Task UpdateAsync(WeatherForecast weatherForecast);
    void Delete(Guid id);
    void Delete(WeatherForecast weatherForecast);
    Task DeleteAsync(Guid id);
    Task DeleteAsync(WeatherForecast? weatherForecast);
}
