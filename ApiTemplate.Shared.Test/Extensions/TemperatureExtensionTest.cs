using ApiTemplate.Shared.Extensions;

namespace ApiTemplate.Shared.Test.Extensions;

public class TemperatureExtensionTest
{
    [Fact]
    public void ToCelsius_Given_Fahrenheit_Returns_Celsius()
    {
        // Arrange
        var celsius = 37.77777777777778m;

        // Act
        var fahrenheit = celsius.ToFahrenheit();

        // Assert
        Assert.Equal(100, fahrenheit);
    }
}
