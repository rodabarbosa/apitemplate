using ApiTemplate.Domain.Entities;
using System.Security.Cryptography;

namespace ApiTemplate.Infra.Data.Seeds;

public static class WeatherForecastSeed
{
    private static readonly DateTime minDate = new DateTime(1900, 1, 1);
    private static readonly DateTime maxDate = DateTime.Now;

    private static readonly string[] _summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

    public static IEnumerable<WeatherForecast> GetSeeds()
    {
        using var rnd = RandomNumberGenerator.Create();

        var lista = new List<WeatherForecast>
        {
            new()
            {
                Id = Guid.Parse("10fd1392-3b4c-431a-b6dc-19cfba4ea269"), // Delete Test
                Date = GetRandomDate(rnd),
                TemperatureCelsius = GetRandomValue(rnd, -20, 55),
                Summary = _summaries[GetRandomValue(rnd, 0, _summaries.Length)]
            },
            new()
            {
                Id = Guid.Parse("43903282-c4b3-42f9-99cc-fd234ee6941d"), // Update Test
                Date = GetRandomDate(rnd),
                TemperatureCelsius = 30,
                Summary = _summaries[GetRandomValue(rnd, 0, _summaries.Length)]
            }
        };

        for (var i = 0; i < 5; i++)
        {
            var weather = new WeatherForecast
            {
                Id = Guid.NewGuid(),
                Date = GetRandomDate(rnd),
                TemperatureCelsius = GetRandomValue(rnd, -20, 55),
                Summary = _summaries[GetRandomValue(rnd, 0, _summaries.Length)]
            };

            lista.Add(weather);
        }

        return lista;
    }

    private static DateTime GetRandomDate(RandomNumberGenerator rnd)
    {
        long randomTicks = GetRandomValue(rnd, minDate.Ticks, maxDate.Ticks);

        return new DateTime(randomTicks);
    }

    private static long GetRandomValue(RandomNumberGenerator rnd, long minValue, long maxValue)
    {
        var randomBytes = new byte[8];
        rnd.GetBytes(randomBytes);

        long randomValue = BitConverter.ToInt64(randomBytes, 0);
        return (randomValue % (maxValue - minValue)) + minValue;
    }
}
