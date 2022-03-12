using System.Linq.Expressions;
using ApiTemplate.Domain.Entities;
using ApiTemplate.Domain.Interfaces;

namespace ApiTemplate.Infra.Data.Repositories;

public class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly ApiTemplateContext _context;

    public WeatherForecastRepository(ApiTemplateContext context)
    {
        _context = context;
    }

    public IQueryable<WeatherForecast> Get(Expression<Func<WeatherForecast, bool>> predicate = null)
    {
        if (predicate == null)
            return _context.WeatherForecasts;

        return _context.WeatherForecasts.Where(predicate);
    }

    public WeatherForecast Get(Guid id) => _context.WeatherForecasts.SingleOrDefault(x => x.Id == id);

    public WeatherForecast Add(WeatherForecast weatherForecast)
    {
        if (weatherForecast.Id == Guid.Empty)
            weatherForecast.Id = Guid.NewGuid();

        _context.WeatherForecasts.Add(weatherForecast);
        return weatherForecast;
    }

    public WeatherForecast Update(WeatherForecast weatherForecast)
    {
        _context.WeatherForecasts.Update(weatherForecast);
        return weatherForecast;
    }

    public void Delete(Guid id)
    {
        var weather = _context.WeatherForecasts.SingleOrDefault(x => x.Id == id);
        if (weather == null) return;

        Delete(weather);
    }

    public void Delete(WeatherForecast weatherForecast) => _context.WeatherForecasts.Remove(weatherForecast);
}
