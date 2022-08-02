using System.Linq.Expressions;
using ApiTemplate.Domain.Entities;
using ApiTemplate.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Infra.Data.Repositories;

public sealed class WeatherForecastRepository : IWeatherForecastRepository
{
    private readonly ApiTemplateContext _context;

    public WeatherForecastRepository(ApiTemplateContext context)
    {
        _context = context;
    }

    public IQueryable<WeatherForecast> Get(Expression<Func<WeatherForecast, bool>>? predicate = default)
    {
        return predicate == null
            ? _context.WeatherForecasts
            : _context.WeatherForecasts.Where(predicate);
    }

    public WeatherForecast? Get(Guid id) => _context.WeatherForecasts.SingleOrDefault(x => x.Id == id);

    public Task<WeatherForecast?> GetAsync(Guid id) => _context.WeatherForecasts.SingleOrDefaultAsync(x => x.Id == id);

    public void Add(WeatherForecast weatherForecast)
    {
        AddChechup(weatherForecast);
        _context.SaveChanges();
    }

    public Task AddAsync(WeatherForecast weatherForecast)
    {
        AddChechup(weatherForecast);
        return _context.SaveChangesAsync();
    }

    private void AddChechup(WeatherForecast weatherForecast)
    {
        if (weatherForecast.Id == Guid.Empty)
            weatherForecast.Id = Guid.NewGuid();

        _context.WeatherForecasts.Add(weatherForecast);
    }

    public void Update(WeatherForecast weatherForecast)
    {
        _context.WeatherForecasts.Update(weatherForecast);
        _context.SaveChanges();
    }

    public Task UpdateAsync(WeatherForecast weatherForecast)
    {
        _context.WeatherForecasts.Update(weatherForecast);
        return _context.SaveChangesAsync();
    }

    public void Delete(Guid id)
    {
        WeatherForecast? weather = _context.WeatherForecasts.SingleOrDefault(x => x.Id == id);
        if (weather == null)
            return;

        Delete(weather);
    }

    public Task DeleteAsync(Guid id)
    {
        WeatherForecast? weather = _context.WeatherForecasts.SingleOrDefault(x => x.Id == id);
        if (weather == null)
            return Task.CompletedTask;

        return DeleteAsync(weather);
    }

    public void Delete(WeatherForecast weatherForecast)
    {
        _context.WeatherForecasts.Remove(weatherForecast);
        _context.SaveChanges();
    }

    public Task DeleteAsync(WeatherForecast weatherForecast)
    {
        _context.WeatherForecasts.Remove(weatherForecast);
        return _context.SaveChangesAsync();
    }
}
