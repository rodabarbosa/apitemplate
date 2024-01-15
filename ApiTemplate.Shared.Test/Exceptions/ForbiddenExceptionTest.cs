namespace ApiTemplate.Shared.Test.Exceptions;

public class ForbiddenExceptionTest
{
    [Fact]
    public void ForbiddenException_Should_Be_Created()
    {
        // Act
        var exception = new ForbiddenException();

        // Assert
        exception.Should()
            .NotBeNull();
    }

    [Fact]
    public void ForbiddenException_Should_Have_Correct_Inner_Exception()
    {
        // Arrange
        var expectedInnerException = new Exception();

        // Act
        var exception = new ForbiddenException(expectedInnerException);

        // Assert
        exception.InnerException
            .Should()
            .Be(expectedInnerException);
    }

    [Fact]
    public void ForbiddenException_Should_Have_Correct_Message()
    {
        // Arrange
        var expectedMessage = "Access Forbidden";

        // Act
        var exception = new ForbiddenException(expectedMessage);

        // Assert
        exception.Message
            .Should()
            .Be(expectedMessage);
    }

    [Theory]
    [InlineData(true, "Exception message")]
    [InlineData(true, "")]
    public void Should_Throw_ForbiddenException_When_Meets_Condition(bool condition, string message)
    {
        var act = () => ForbiddenException.ThrowIf(condition, message);
        act.Should()
            .Throw<ForbiddenException>();
    }

    [Theory]
    [InlineData(false, "Exception message")]
    public void Should_Not_Throw_ForbiddenException_When_Meets_Condition(bool condition, string message)
    {
        var act = () => ForbiddenException.ThrowIf(condition, message);

        act.Should()
            .NotThrow();
    }
}
