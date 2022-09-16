using ApiTemplate.Shared.Extensions;

namespace ApiTemplate.Shared.Test.Extensions;

public class TemperatureExtensionTest
{
    [Fact]
    public void ToCelsius_Given_Fahrenheit_Returns_Celsius()
    {
        // Arrange
        var celsius = 37m;

        // Act
        var fahrenheit = celsius.ToFahrenheit();

        // Assert
        Assert.Equal(98.6m, fahrenheit);
    }

    [Fact]
    public void ToFahrenheit_Given_Celsius_Returns_Fahrenheit()
    {
        // Arrange
        var fahrenheit = 98.6m;

        // Act
        var celsius = fahrenheit.ToCelsius();

        // Assert
        Assert.Equal(37m, celsius);
    }
}
