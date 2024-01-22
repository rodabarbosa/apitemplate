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

        result.Should()
            .Be(operation);
    }

    [Theory]
    [InlineData("date=[notequal,2019-01-01]&TemperatureCelsius=[greaterthan,0]&TemperatureFahrenheit=[equal,32]")]
    [InlineData("date=[equal,2019-01-01]")]
    public void Should_Return_Value_Data_Extract_From_Query(string query)
    {
        // Arrange
        var dateOperation = query.ExtractDateParam();

        dateOperation.Should()
            .NotBeNull();

        dateOperation!.Value
            .Should()
            .Be(new DateTime(2019, 1, 1));
    }

    [Theory]
    [InlineData("date=[notequal,2019-01-01]&TemperatureCelsius=[greaterthan,0]&TemperatureFahrenheit=[equal,32]")]
    public void Should_Return_Value_Celsius_Extract_From_Query(string query)
    {
        var celsiusOperation = query.ExtractTemperatureCelsiusParam();

        celsiusOperation.Should()
            .NotBeNull();

        celsiusOperation!.Operation
            .Should()
            .Be(Operation.GreaterThan);

        celsiusOperation!.Value
            .Should()
            .Be(0);
    }

    [Theory]
    [InlineData("date=[notequal,2019-01-01]&TemperatureCelsius=[greaterthan,0]&TemperatureFahrenheit=[equal,32]")]
    public void Should_Return_Value_Fahrenheit_Extract_From_Query(string query)
    {
        var fahrenheitOperation = query.ExtractTemperatureFahrenheitParam();

        fahrenheitOperation.Should()
            .NotBeNull();

        fahrenheitOperation!.Operation
            .Should()
            .Be(Operation.Equal);

        fahrenheitOperation!.Value
            .Should()
            .Be(32);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("day=[equal,2019-01-01]")]
    public void Should_Not_Return_Value_Data_Extract_From_Query(string? query)
    {
        // Arrange
        var dateOperation = query.ExtractDateParam();

        dateOperation.Should()
            .BeNull();

        var celsiusOperation = query.ExtractTemperatureCelsiusParam();

        celsiusOperation.Should()
            .BeNull();

        var fahrenheitOperation = query?.ExtractTemperatureFahrenheitParam();

        fahrenheitOperation.Should()
            .BeNull();
    }
}
