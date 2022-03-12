using ApiTemplate.Domain.Entities;

namespace ApiTemplate.Infra.Data.Seeds;

public static class WeatherForecastSeed
{
    private static readonly string[] Summaries = {"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"};

    public static List<WeatherForecast> GetSeeds()
    {
        var rnd = new Random();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Id = Guid.NewGuid(),
                Date = GetRandomDate(rnd),
                TemperatureC = rnd.Next(-20, 55),
                Summary = Summaries[rnd.Next(Summaries.Length)]
            })
            .ToList();
    }

    private static DateTime GetRandomDate(Random rnd)
    {
        int rndYear = rnd.Next(1995, DateTime.Now.Year);
        int rndMonth = rnd.Next(1, 12);
        int rndDay = rnd.Next(1, 31);

        return new DateTime(rndYear, rndMonth, rndDay);
    }
}
