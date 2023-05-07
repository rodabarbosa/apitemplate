namespace ApiTemplate.Shared.Test.Exceptions;

public class NotificationFailureExceptionTest
{
    [Fact]
    public void NotificationFailureException_Should_Be_Created()
    {
        // Act
        var exception = new NotificationFailureException();

        // Assert
        exception.Should()
            .NotBeNull();
    }

    [Fact]
    public void NotificationFailureException_Should_Have_Correct_Inner_Exception()
    {
        // Arrange
        var expectedInnerException = new Exception();

        // Act
        var exception = new NotificationFailureException(expectedInnerException);

        // Assert
        exception.InnerException!
            .Message
            .Should()
            .Be(expectedInnerException.Message);
    }

    [Fact]
    public void NotificationFailureException_Should_Have_Correct_Message()
    {
        // Arrange
        var expectedMessage = "Notification has failed";

        // Act
        var exception = new NotificationFailureException(expectedMessage);

        // Assert
        exception.Message
            .Should()
            .Be(expectedMessage);
    }

    [Theory]
    [InlineData(true, "Exception message")]
    [InlineData(true, "")]
    public void Should_Throw_NotificationFailureException_When_Meets_Condition(bool condition, string message)
    {
        var act = () => NotificationFailureException.ThrowIf(condition, message);

        act.Should()
            .Throw<NotificationFailureException>();
    }

    [Theory]
    [InlineData(false, "Exception message")]
    public void Should_Not_Throw_NotificationFailureException_When_Meets_Condition(bool condition, string message)
    {
        var act = () => NotificationFailureException.ThrowIf(condition, message);

        act.Should()
            .NotThrow();
    }

    [Fact]
    public void NotFoundException_Serialization()
    {
        // Arrange
        var expectedMessage = "Serialization test";
        var e = new NotificationFailureException(expectedMessage);

        // Act
        using (Stream s = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011
            formatter.Serialize(s, e);
            s.Position = 0; // Reset stream position
            e = (NotificationFailureException)formatter.Deserialize(s);
#pragma warning restore SYSLIB0011
        }

        // Assert
        e.Message
            .Should()
            .Be(expectedMessage);
    }
}
