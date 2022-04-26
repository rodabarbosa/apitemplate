using ApiTemplate.Shared.Extensions;
using Xunit;

namespace ApiTemplate.Tests.SharedProjectTest.Extensions;

public class TemperatureExtensionTest
{
    [Fact]
    public void ToCelsius_Given_Fahrenheit_Returns_Celsius()
    {
        // Arrange
        double celsius = 37.77777777777778;

        // Act
        var fahrenheit = celsius.ToFahrenheit();

        // Assert
        Assert.Equal(100, fahrenheit);
    }
}