namespace ApiTemplate.Shared.Test.Exceptions;

public class DeleteFailureExceptionTest
{
    [Fact]
    public void DeleteFailureException_Should_Be_Created()
    {
        // Act
        var exception = new DeleteFailureException();

        // Assert
        exception.Should()
            .NotBeNull();
    }

    [Fact]
    public void DeleteFailureException_Should_Have_Correct_Inner_Exception()
    {
        // Arrange
        var expectedInnerException = new Exception();

        // Act
        var exception = new DeleteFailureException(expectedInnerException);

        // Assert
        exception.InnerException
            .Should()
            .Be(expectedInnerException);
    }

    [Fact]
    public void DeleteFailureException_Should_Have_Correct_Message()
    {
        // Arrange
        var expectedMessage = "Delete has failed";

        // Act
        var exception = new DeleteFailureException(expectedMessage);

        // Assert
        exception.Message
            .Should()
            .Be(expectedMessage);
    }

    [Theory]
    [InlineData(true, "Exception message")]
    [InlineData(true, "")]
    public void Should_Throw_DeleteFailureException_When_Meets_Condition(bool condition, string message)
    {
        var act = () => DeleteFailureException.ThrowIf(condition, message);

        act.Should()
            .Throw<DeleteFailureException>();
    }

    [Theory]
    [InlineData(false, "Exception message")]
    public void Should_Not_Throw_DeleteFailureException_When_Meets_Condition(bool condition, string message)
    {
        var act = () => DeleteFailureException.ThrowIf(condition, message);

        act.Should()
            .NotThrow();
    }

    [Fact]
    public void DeleteFailureException_Serialization()
    {
        // Arrange
        var expectedMessage = "Serialization test";
        var e = new DeleteFailureException(expectedMessage);

        // Act
        using (Stream s = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011
            formatter.Serialize(s, e);
            s.Position = 0; // Reset stream position
            e = (DeleteFailureException)formatter.Deserialize(s);
#pragma warning restore SYSLIB0011
        }

        // Assert
        e.Message
            .Should()
            .Be(expectedMessage);
    }
}
