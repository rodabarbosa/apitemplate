namespace ApiTemplate.Shared.Test.Exceptions;

public class DbRegisterExceptionTest
{
    [Fact]
    public void DbRegisterException_Should_Be_Created()
    {
        // Act
        var exception = new DbRegisterExistsException();

        // Assert
        exception.Should()
            .NotBeNull();
    }

    [Fact]
    public void DbRegisterException_Should_Have_Correct_Inner_Exception()
    {
        // Arrange
        var expectedInnerException = new Exception();

        // Act
        var exception = new DbRegisterExistsException(expectedInnerException);

        // Assert
        exception.InnerException
            .Should()
            .Be(expectedInnerException);
    }

    [Fact]
    public void DbRegisterException_Should_Have_Correct_Message()
    {
        // Arrange
        var expectedMessage = "Database registration failed.";

        // Act
        var exception = new DbRegisterExistsException(expectedMessage);

        // Assert
        exception.Message
            .Should()
            .Be(expectedMessage);
    }

    [Theory]
    [InlineData(true, "Exception message")]
    [InlineData(true, "")]
    public void Should_Throw_DbRegisterExistsException_When_Meets_Condition(bool condition, string message)
    {
        var act = () => DbRegisterExistsException.ThrowIf(condition, message);

        act.Should()
            .Throw<DbRegisterExistsException>();
    }

    [Theory]
    [InlineData(false, "Exception message")]
    public void Should_Not_Throw_DbRegisterExistsException_When_Meets_Condition(bool condition, string message)
    {
        var act = () => DbRegisterExistsException.ThrowIf(condition, message);

        act.Should()
            .NotThrow();
    }

    [Fact]
    public void DbRegisterExistsException_Serialization()
    {
        // Arrange
        const string expectedMessage = "Serialization test";
        var e = new DbRegisterExistsException(expectedMessage);

        // Act
        using (Stream s = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011
            formatter.Serialize(s, e);
            s.Position = 0; // Reset stream position
            e = (DbRegisterExistsException)formatter.Deserialize(s);
#pragma warning restore SYSLIB0011
        }

        // Assert
        e.Message
            .Should()
            .Be(expectedMessage);
    }
}
