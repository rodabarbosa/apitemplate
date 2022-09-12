using ApiTemplate.Domain.Entities;
using ApiTemplate.Domain.Repositories;
using ApiTemplate.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
        return predicate is null
            ? _context.WeatherForecasts!
            : _context.WeatherForecasts!.Where(predicate);
    }

    public WeatherForecast? Get(Guid id)
    {
        return _context.WeatherForecasts!.SingleOrDefault(x => x.Id == id);
    }

    public Task<WeatherForecast?> GetAsync(Guid id)
    {
        return _context.WeatherForecasts!.SingleOrDefaultAsync(x => x.Id == id);
    }

    public void Add(WeatherForecast weatherForecast)
    {
        AddCheckup(weatherForecast);
        _context.SaveChanges();
    }

    public Task AddAsync(WeatherForecast weatherForecast)
    {
        AddCheckup(weatherForecast);
        return _context.SaveChangesAsync();
    }

    public void Update(WeatherForecast weatherForecast)
    {
        _context.WeatherForecasts!.Update(weatherForecast);
        _context.SaveChanges();
    }

    public Task UpdateAsync(WeatherForecast weatherForecast)
    {
        _context.WeatherForecasts!.Update(weatherForecast);
        return _context.SaveChangesAsync();
    }

    public void Delete(Guid id)
    {
        var weather = Get(id);

        DeleteFailureException.ThrowIf(weather is null, $"Weather forecast {id} not found");

        Delete(weather);
    }

    public Task DeleteAsync(Guid id)
    {
        var weather = Get(id);

        DeleteFailureException.ThrowIf(weather is null, $"Weather forecast {id} not found");

        return DeleteAsync(weather);
    }

    public void Delete(WeatherForecast? weatherForecast)
    {
        DeleteFailureException.ThrowIf(weatherForecast is null, "Weather forecast was not informed.");

        _context.WeatherForecasts!.Remove(weatherForecast!);
        _context.SaveChanges();
    }

    public Task DeleteAsync(WeatherForecast? weatherForecast)
    {
        DeleteFailureException.ThrowIf(weatherForecast is null, "Weather forecast was not informed.");

        _context.WeatherForecasts!.Remove(weatherForecast!);
        return _context.SaveChangesAsync();
    }

    private void AddCheckup(WeatherForecast weatherForecast)
    {
        if (weatherForecast.Id == Guid.Empty || weatherForecast.Id == default)
            weatherForecast.Id = Guid.NewGuid();

        _context.WeatherForecasts!.Add(weatherForecast);
    }
}
