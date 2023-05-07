namespace ApiTemplate.Domain.Test.Entities;

public class WeatherForecastTest
{
    [Theory]
    [InlineData(true, true, "Test summary")]
    [InlineData(false, false, null)]
    public void ShouldBeAbleToCreateAWeatherForecast(bool generateId, bool getDateNow, string? summary)
    {
        var weather = new WeatherForecast
        {
            Summary = summary
        };

        if (generateId)
            weather.Id = Guid.NewGuid();

        if (getDateNow)
            weather.Date = DateTime.Now;

        weather.Should()
            .NotBeNull();

        weather.Id
            .Should()
            .NotBe(Guid.Empty);

        weather.Date
            .Should()
            .NotBe(DateTime.MinValue)
            .And
            .NotBe(default);

        weather.Summary
            .Should()
            .Be(summary);
    }

    [Fact]
    public void ShouldBeAbleToCreateAWeatherForecastWithId()
    {
        var weatherForecast = new WeatherForecast
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
            TemperatureCelsius = 10,
            Summary = "Test summary"
        };

        weatherForecast.Id
            .Should()
            .NotBe(Guid.Empty);

        weatherForecast.Date
            .Should()
            .NotBe(DateTime.MinValue)
            .And
            .NotBe(default);

        weatherForecast.TemperatureCelsius
            .Should()
            .Be(10);

        weatherForecast.Summary
            .Should()
            .Be("Test summary");
    }
}
