namespace ApiTemplate.Application.Test.Contracts;

public class GetWeatherForecastResponseTest
{
    [Fact]
    public void GetWeatherForecastResponse_ShouldBeCreated()
    {
        var response = new GetWeatherForecastResponseContract
        {
            Date = DateTime.Now,
            TemperatureCelsius = 0,
            Summary = "summary"
        };

        response.TemperatureFahrenheit
            .Should()
            .Be(32);
    }
}
