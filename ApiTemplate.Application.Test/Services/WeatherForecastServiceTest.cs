namespace ApiTemplate.Application.Test.Services;

public sealed class WeatherForecastServiceTest : IDisposable
{
    private readonly ApiTemplateContext _context;
    private readonly IWeatherForecastRepository _weatherForecastRepository;

    public WeatherForecastServiceTest()
    {
        _context = ContextUtil.GetContext();
        _weatherForecastRepository = new WeatherForecastRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Theory]
    [InlineData("43903282-c4b3-42f9-99cc-fd234ee6941d")]
    async public Task Should_Return_Weather_Collection(Guid id)
    {
        var service = new GetWeatherForecastService(_weatherForecastRepository);

        var act = () => service.GetWeatherForecastAsync(id, CancellationToken.None);

        await act.Should()
            .NotThrowAsync();
    }

    [Theory]
    [InlineData("43903282-0000-0000-0000-fd234ee6941d")]
    async public Task Should_Not_Return_Weather_Collection(Guid id)
    {
        // Act
        var service = new GetWeatherForecastService(_weatherForecastRepository);

        var act = () => service.GetWeatherForecastAsync(id, CancellationToken.None);

        await act.Should()
            .ThrowAsync<NotFoundException>();
    }

    [Theory]
    [InlineData("date", "2030-01-01 00:00:00", "Equal")]
    [InlineData("date", "2030-01-01 00:00:00", "GreaterThan")]
    [InlineData("date", "2030-01-01 00:00:00", "GreaterThanOrEqual")]
    [InlineData("date", "1900-01-01 00:00:00", "LessThan")]
    [InlineData("date", "1900-01-01 00:00:00", "LessThanOrEqual")]
    [InlineData("date", "1900-01-01 00:00:00", "Less")]
    [InlineData("temperatureFahrenheit", "2000", "Equal")]
    [InlineData("temperatureFahrenheit", "2000", "GreaterThan")]
    [InlineData("temperatureFahrenheit", "2000", "GreaterThanOrEqual")]
    [InlineData("temperatureFahrenheit", "-2000", "LessThan")]
    [InlineData("temperatureFahrenheit", "-2000", "LessThanOrEqual")]
    [InlineData("temperatureFahrenheit", "-2000", "Less")]
    [InlineData("temperatureCelsius", "2000", "Equal")]
    [InlineData("temperatureCelsius", "2000", "GreaterThan")]
    [InlineData("temperatureCelsius", "2000", "GreaterThanOrEqual")]
    [InlineData("temperatureCelsius", "-2000", "LessThan")]
    [InlineData("temperatureCelsius", "-2000", "LessThanOrEqual")]
    [InlineData("temperatureCelsius", "-2000", "Less")]
    async public Task GetForecast_Returns_WeatherForecast_With_Filter(string key, string value, string operation)
    {
        var service = new GetAllWeatherForecastsService(_weatherForecastRepository);

        var param = $"dtInsert=[Equal,0]&dtUpdate=[Equal,0]&{key}=[{operation},{value}]";

        var result = await service.GetAllWeatherForecastsAsync(param, CancellationToken.None);

        result.Should()
            .NotBeNull()
            .And
            .BeEmpty();
    }

    [Theory]
    [InlineData("date", "2030-01-01 00:00:00")]
    [InlineData("temperatureFahrenheit", "2000")]
    [InlineData("temperatureCelsius", "2000")]
    async public Task GetForecast_Returns_WeatherForecast_With_Filter_NotEqual(string key, string value)
    {
        var service = new GetAllWeatherForecastsService(_weatherForecastRepository);

        var param = $"{key}=[NotEqual,{value}]";

        var result = await service.GetAllWeatherForecastsAsync(param, CancellationToken.None);

        result.Should()
            .NotBeNull()
            .And
            .NotBeEmpty();
    }

    [Fact]
    async public Task WeatherForecast_Creating()
    {
        // Arrange
        var service = new CreateWeatherForecastService(_weatherForecastRepository);

        var weatherForecast = new CreateWeatherForecastRequestContract
        {
            Date = DateTime.Now.AddHours(-1),
            TemperatureCelsius = 10,
            Summary = "Summary"
        };

        // Act
        var result = await service.CreateWeatherForecastAsync(weatherForecast, CancellationToken.None);

        result.Should()
            .NotBeNull();
    }

    [Fact]
    async public Task WeatherForecast_Updating()
    {
        // Arrange
        var service = new UpdateWeatherForecastService(_weatherForecastRepository);

        var id = Guid.Parse("43903282-c4b3-42f9-99cc-fd234ee6941d");

        var celsius = 30m;

        var request = new UpdateWeatherForecastRequestContract
        {
            Id = id,
            Date = DateTime.Now.AddHours(-1),
            TemperatureCelsius = celsius,
            TemperatureFahrenheit = celsius.ToFahrenheit(),
            Summary = default
        };

        var act = () => service.UpdateWeatherForecastAsync(id, request, CancellationToken.None);

        await act.Should()
            .NotThrowAsync();
    }

    [Fact]
    async public Task WeatherForecast_Deleting()
    {
        // Arrange
        var service = new DeleteWeatherForecastService(_weatherForecastRepository);

        // Act
        var id = Guid.Parse("10fd1392-3b4c-431a-b6dc-19cfba4ea269");

        // Assert
        var act = () => service.DeleteWeatherForecastAsync(id, CancellationToken.None);

        await act.Should()
            .NotThrowAsync();
    }
}
