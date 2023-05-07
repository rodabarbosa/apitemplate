namespace ApiTemplate.Shared.Test.Exceptions;

public class NotFoundExceptionTest
{
    [Fact]
    public void NotFoundException_Should_Be_Created()
    {
        // Act
        var exception = new NotFoundException();

        // Assert
        exception.Should()
            .NotBeNull();
    }

    [Fact]
    public void NotFoundException_Should_Have_Correct_Inner_Exception()
    {
        // Arrange
        var expectedInnerException = new Exception();

        // Act
        var exception = new NotFoundException(expectedInnerException);

        // Assert
        exception.InnerException!
            .Message
            .Should()
            .Be(expectedInnerException.Message);
    }

    [Fact]
    public void NotFoundException_Should_Have_Correct_Message()
    {
        // Arrange
        var expectedMessage = "Not Found Exception";

        // Act
        var exception = new NotFoundException(expectedMessage);

        // Assert
        exception.Message
            .Should()
            .Be(expectedMessage);
    }

    [Theory]
    [InlineData(true, "Exception message")]
    [InlineData(true, "")]
    public void Should_Throw_NotFoundException_When_Meets_Condition(bool condition, string message)
    {
        var act = () => NotFoundException.ThrowIf(condition, message);

        act.Should()
            .Throw<NotFoundException>();
    }

    [Theory]
    [InlineData(false, "Exception message")]
    public void Should_Not_Throw_NotFoundException_When_Meets_Condition(bool condition, string message)
    {
        var act = () => NotFoundException.ThrowIf(condition, message);

        act.Should()
            .NotThrow();
    }

    [Fact]
    public void NotFoundException_Serialization()
    {
        // Arrange
        var expectedMessage = "Serialization test";
        var e = new NotFoundException(expectedMessage);

        // Act
        using (Stream s = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011
            formatter.Serialize(s, e);
            s.Position = 0; // Reset stream position
            e = (NotFoundException)formatter.Deserialize(s);
#pragma warning restore SYSLIB0011
        }

        // Assert
        e.Message
            .Should()
            .Be(expectedMessage);
    }
}
