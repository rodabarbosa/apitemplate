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
        fahrenheit.Should()
            .Be(98.6m);
    }

    [Fact]
    public void ToFahrenheit_Given_Celsius_Returns_Fahrenheit()
    {
        // Arrange
        var fahrenheit = 98.6m;

        // Act
        var celsius = fahrenheit.ToCelsius();

        // Assert
        celsius.Should()
            .Be(37m);
    }
}
