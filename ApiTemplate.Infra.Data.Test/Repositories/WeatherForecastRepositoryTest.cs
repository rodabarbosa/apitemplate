namespace ApiTemplate.Infra.Data.Test.Repositories;

public class WeatherForecastRepositoryTest
{
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public WeatherForecastRepositoryTest()
    {
        var context = ContextUtil.GetContext();
        _weatherForecastRepository = new WeatherForecastRepository(context);
    }

    [Fact]
    public void GetAll_ShouldReturnAllWeatherForecasts()
    {
        var weatherForecasts = _weatherForecastRepository.Get();
        weatherForecasts.Should()
            .NotBeEmpty();
    }

    [Fact]
    public void GetByFilter_ShouldReturnWeatherForecast()
    {
        var weatherForecasts = _weatherForecastRepository.Get();

        var weatherForecast = weatherForecasts.FirstOrDefault();
        Assert.NotNull(weatherForecast);

        var temperature = weatherForecast?.TemperatureCelsius ?? 0;
        var result = _weatherForecastRepository.Get(x => x.TemperatureCelsius < temperature).FirstOrDefault();

        result.Should()
            .NotBeNull();
    }

    [Fact]
    public void GetById_ShouldReturnWeatherForecast()
    {
        var weatherForecasts = _weatherForecastRepository.Get();

        var weatherForecast = weatherForecasts.First();

        var result = _weatherForecastRepository.Get(x => x.Id == weatherForecast.Id).FirstOrDefault();

        result.Should()
            .NotBeNull();
    }

    [Fact]
    public void Adding_WeatherForecast()
    {
        var defaultId = Guid.Empty;
        var weather = new WeatherForecast
        {
            Id = defaultId,
            Date = DateTime.Today,
            TemperatureCelsius = 10,
            Summary = "Test Summary"
        };

        _weatherForecastRepository.Add(weather);

        weather.Id.Should()
            .NotBe(defaultId);
    }

    [Fact]
    public void Updating_WeatherForecast()
    {
        var weatherForecasts = _weatherForecastRepository.Get();

        var weatherForecast = weatherForecasts.First();

        weatherForecast.TemperatureCelsius = 20;

        _weatherForecastRepository.Update(weatherForecast);

        var result = _weatherForecastRepository.Get(x => x.TemperatureCelsius < weatherForecast.TemperatureCelsius).FirstOrDefault();

        result.Should()
            .NotBeNull();
    }

    [Fact]
    public void Deleting_WeatherForecast()
    {
        var weatherForecasts = _weatherForecastRepository.Get();

        var weatherForecast = weatherForecasts.First();

        _weatherForecastRepository.Delete(weatherForecast.Id);

        var result = _weatherForecastRepository.Get(x => x.Id == weatherForecast.Id).FirstOrDefault();

        result.Should()
            .BeNull();
    }
}
