using System;
using ApiTemplate.Application.Enumerators;
using ApiTemplate.WebApi.Extensions;
using Xunit;

namespace ApiTemplate.Tests.WebApiProjectTest.Extensions;

public class WeatherForecastResourceExtensionTest
{
    [Theory]
    [InlineData("date=[notequal,2019-01-01]&TemperatureC=[greaterthan,0]&TemperatureF=[equal,32]")]
    public void ToWeatherForecastResource_ShouldReturnCorrectResource(string query)
    {
        // Arrange
        var dateOperation = query.ExtractDateParam();

        Assert.NotNull(dateOperation);
        Assert.Equal(Operation.NotEqual, dateOperation.Operation);
        Assert.Equal(new DateTime(2019, 1, 1), dateOperation.Value);

        var celsiusOperation = query.ExtractTemperatureCelsiusParam();
        Assert.NotNull(celsiusOperation);
        Assert.Equal(Operation.GreaterThan, celsiusOperation.Operation);
        Assert.Equal(0, celsiusOperation.Value);

        var fahrenheitOperation = query.ExtractTemperatureFahrenheitParam();
        Assert.NotNull(fahrenheitOperation);
        Assert.Equal(Operation.Equal, fahrenheitOperation.Operation);
        Assert.Equal(32, fahrenheitOperation.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ToWeatherForecastResource_ShouldReturnNull(string query)
    {
        // Arrange
        var dateOperation = query.ExtractDateParam();

        Assert.Null(dateOperation);

        var celsiusOperation = query.ExtractTemperatureCelsiusParam();
        Assert.Null(celsiusOperation);

        var fahrenheitOperation = query.ExtractTemperatureFahrenheitParam();
        Assert.Null(fahrenheitOperation);
    }

    [Fact]
    public void Test()
    {
    }
}