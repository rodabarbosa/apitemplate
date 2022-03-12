using System;
using System.Linq;
using System.Linq.Expressions;
using ApiTemplate.Domain.Entities;

namespace ApiTemplate.Domain.Interfaces;

public interface IWeatherForecastRepository
{
    IQueryable<WeatherForecast> Get(Expression<Func<WeatherForecast, bool>> predicate = null);
    WeatherForecast Get(Guid id);
    WeatherForecast Add(WeatherForecast weatherForecast);
    WeatherForecast Update(WeatherForecast weatherForecast);
    void Delete(Guid id);
    void Delete(WeatherForecast weatherForecast);
}
