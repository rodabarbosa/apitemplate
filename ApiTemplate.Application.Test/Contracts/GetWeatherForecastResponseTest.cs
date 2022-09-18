using ApiTemplate.Application.Contracts;

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

        Assert.Equal(32, response.TemperatureFahrenheit);
    }
}
