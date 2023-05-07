namespace ApiTemplate.Shared.Test.Models;

public class ErrorModelTest
{
    [Fact]
    public void ErrorModel_Should_Be_Created()
    {
        var errorModel = new ErrorModel
        {
            Code = 0,
            Error = null,
            Exception = null,
            StackTrace = null
        };

        errorModel.Should()
            .NotBeNull();
    }

    [Fact]
    public void ErrorModel_Should_Be_Created_With_Exception()
    {
        var exception = new Exception("Test");
        var errorModel = new ErrorModel
        {
            Code = 0,
            Error = exception.Message,
            Exception = exception.GetType().Name,
            StackTrace = exception.StackTrace
        };

        errorModel.Should()
            .NotBeNull();
    }

    [Fact]
    public void ErrorModel_Should_Not_BeEmpty()
    {
        var errorModel = new ErrorModel
        {
            Code = 1,
            Error = "Error test",
            Exception = "Exception test",
            StackTrace = "stack trace test"
        };

        errorModel.Code
            .Should()
            .NotBe(0);

        errorModel.Error
            .Should()
            .NotBeNull();

        errorModel.Exception
            .Should()
            .NotBeNull()
            .And
            .NotBeEmpty();

        errorModel.StackTrace
            .Should()
            .NotBeNull()
            .And
            .NotBeEmpty();
    }
}
