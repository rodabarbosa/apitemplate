using ApiTemplate.Application.Enumerators;
using ApiTemplate.Application.Extensions;

namespace ApiTemplate.Application.Test.Extensions;

public class OperationExtensionTest
{
    [Theory]
    [InlineData("equal", Operation.Equal)]
    [InlineData("notequal", Operation.NotEqual)]
    [InlineData("greaterthan", Operation.GreaterThan)]
    [InlineData("greaterthanorequal", Operation.GreaterThanOrEqual)]
    [InlineData("lessthan", Operation.LessThan)]
    [InlineData("lessthanorequal", Operation.LessThanOrEqual)]
    [InlineData("contains", Operation.Contains)]
    [InlineData("startswith", Operation.StartsWith)]
    [InlineData("endswith", Operation.EndsWith)]
    [InlineData("notlisted", Operation.Equal)]
    public void Should_Return_True_When_Operation_Is_Equal(string operationName, Operation operation)
    {
        var result = operationName.ToOperation();
        Assert.Equal(result, operation);
    }

    [Theory]
    [InlineData("date=[notequal,2019-01-01]&TemperatureCelsius=[greaterthan,0]&TemperatureFahrenheit=[equal,32]", true)]
    [InlineData("", false)]
    [InlineData("day=[equal,2019-01-01]", false)]
    public void ToWeatherForecastResource_ShouldReturnCorrectResource(string query, bool expected)
    {
        // Arrange
        var dateOperation = query.ExtractDateParam();

        if (!expected)
        {
            Assert.Null(dateOperation);
            return;
        }

        Assert.NotNull(dateOperation);
        Assert.Equal(Operation.NotEqual, dateOperation?.Operation);
        Assert.Equal(new DateTime(2019, 1, 1), dateOperation?.Value);

        var celsiusOperation = query.ExtractTemperatureCelsiusParam();
        Assert.NotNull(celsiusOperation);
        Assert.Equal(Operation.GreaterThan, celsiusOperation?.Operation);
        Assert.Equal(0, celsiusOperation?.Value);

        var fahrenheitOperation = query.ExtractTemperatureFahrenheitParam();
        Assert.NotNull(fahrenheitOperation);
        Assert.Equal(Operation.Equal, fahrenheitOperation?.Operation);
        Assert.Equal(32, fahrenheitOperation?.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("day=[equal,2019-01-01]")]
    public void ToWeatherForecastResource_ShouldReturnNull(string? query)
    {
        // Arrange
        var dateOperation = query.ExtractDateParam();

        Assert.Null(dateOperation);

        var celsiusOperation = query.ExtractTemperatureCelsiusParam();
        Assert.Null(celsiusOperation);

        var fahrenheitOperation = query?.ExtractTemperatureFahrenheitParam();
        Assert.Null(fahrenheitOperation);
    }
}
