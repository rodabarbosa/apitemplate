namespace ApiTemplate.Shared.Test.Exceptions;

public class SaveFailureExceptionTest
{
    [Fact]
    public void SaveFailureException_Should_Be_Created()
    {
        // Act
        var exception = new SaveFailureException();

        // Assert
        exception.Should()
            .NotBeNull();
    }

    [Fact]
    public void SaveFailureException_Should_Have_Correct_Inner_Exception()
    {
        // Arrange
        var expectedInnerException = new Exception();

        // Act
        var exception = new SaveFailureException(expectedInnerException);

        // Assert
        exception.InnerException!
            .Message
            .Should()
            .Be(expectedInnerException.Message);
    }

    [Fact]
    public void SaveFailureException_Should_Have_Correct_Message()
    {
        // Arrange
        var expectedMessage = "Save has failed";

        // Act
        var exception = new SaveFailureException(expectedMessage);

        // Assert
        exception.Message
            .Should()
            .Be(expectedMessage);
    }

    [Theory]
    [InlineData(true, "Exception message")]
    [InlineData(true, "")]
    public void Should_Throw_SaveFailureException_When_Meets_Condition(bool condition, string message)
    {
        var act = () => SaveFailureException.ThrowIf(condition, message);

        act.Should()
            .Throw<SaveFailureException>();
    }

    [Theory]
    [InlineData(false, "Exception message")]
    public void Should_Not_Throw_SaveFailureException_When_Meets_Condition(bool condition, string message)
    {
        var act = () => SaveFailureException.ThrowIf(condition, message);

        act.Should()
            .NotThrow();
    }

    [Fact]
    public void SaveFailureException_Serialization()
    {
        // Arrange
        var expectedMessage = "Serialization test";
        var e = new SaveFailureException(expectedMessage);

        // Act
        using (Stream s = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011
            formatter.Serialize(s, e);
            s.Position = 0; // Reset stream position
            e = (SaveFailureException)formatter.Deserialize(s);
#pragma warning restore SYSLIB0011
        }

        // Assert
        e.Message
            .Should()
            .Be(expectedMessage);
    }
}
