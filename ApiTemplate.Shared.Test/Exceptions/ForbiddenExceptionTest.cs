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

    [Fact]
    public void ForbiddenException_Serialization()
    {
        // Arrange
        var expectedMessage = "Serialization test";
        var e = new ForbiddenException(expectedMessage);

        // Act
        using (Stream s = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011
            formatter.Serialize(s, e);
            s.Position = 0; // Reset stream position
            e = (ForbiddenException)formatter.Deserialize(s);
#pragma warning restore SYSLIB0011
        }

        // Assert
        e.Message
            .Should()
            .Be(expectedMessage);
    }
}
