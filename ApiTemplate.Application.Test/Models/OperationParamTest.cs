namespace ApiTemplate.Application.Test.Models;

public class OperationParamTest
{
    [Fact]
    public void OperationParam_Should_Be_Created()
    {
        var operationParam = new OperationParam<decimal>
        {
            Operation = Operation.Equal,
            Value = 10m
        };

        operationParam.Should()
            .NotBeNull();
    }
}
