namespace ApiTemplate.Shared.Test.Exceptions;

public class UnauthorizedExceptionTest
{
    [Fact]
    public void UnauthorizedException_Should_Be_Created()
    {
        // Act
        var exception = new UnauthorizedException();

        // Assert
        exception.Should()
            .NotBeNull();
    }

    [Fact]
    public void UnauthorizedException_Should_Have_Correct_Inner_Exception()
    {
        // Arrange
        var expectedInnerException = new Exception();

        // Act
        var exception = new UnauthorizedException(expectedInnerException);

        // Assert
        exception.InnerException!
            .Message
            .Should()
            .Be(expectedInnerException.Message);
    }

    [Fact]
    public void UnauthorizedException_Should_Have_Correct_Message()
    {
        // Arrange
        var expectedMessage = "Unauthorized Access";

        // Act
        var exception = new UnauthorizedException(expectedMessage);

        // Assert
        exception.Message
            .Should()
            .Be(expectedMessage);
    }

    [Theory]
    [InlineData(true, "Exception message")]
    [InlineData(true, "")]
    public void Should_Throw_Exception_When_Meets_Condition(bool condition, string message)
    {
        var act = () => UnauthorizedException.ThrowIf(condition, message);

        act.Should()
            .Throw<UnauthorizedException>();
    }

    [Theory]
    [InlineData(false, "Exception message")]
    public void Should_Not_Throw_Exception_When_Meets_Condition(bool condition, string message)
    {
        var act = () => UnauthorizedException.ThrowIf(condition, message);
        act.Should()
            .NotThrow();
    }
}
