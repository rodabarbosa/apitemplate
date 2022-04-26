using ApiTemplate.Application.Enumerators;
using ApiTemplate.Application.Extensions;
using Xunit;

namespace ApiTemplate.Tests.ApplicationProjectTest.Extensions;

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
        Operation result = operationName.ToOperation();
        Assert.Equal(result, operation);
    }
}