using ApiTemplate.Shared.Exceptions;
using System.Runtime.Serialization.Formatters.Binary;

namespace ApiTemplate.Shared.Test.Exceptions;

public class BadRequestExceptionTest
{
    [Fact]
    public void BadRequestException_Should_Be_Created()
    {
        // Act
        var exception = new BadRequestException();

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void BadRequestException_Should_Be_Created_With_InnerException()
    {
        // Act
        var innerException = new Exception();
        var exception = new BadRequestException(innerException);

        // Assert
        Assert.NotNull(exception);
        Assert.NotNull(exception.InnerException);
    }

    [Fact]
    public void BadRequestException_Should_Have_Correct_Inner_Exception()
    {
        // Arrange
        var expectedInnerException = new Exception();

        // Act
        var exception = new DbRegisterExistsException(expectedInnerException);

        // Assert
        Assert.Equal(expectedInnerException, exception.InnerException);
    }

    [Fact]
    public void BadRequestException_Should_Have_Correct_Message()
    {
        // Arrange
        var expectedMessage = "Database registration failed.";

        // Act
        var exception = new DbRegisterExistsException(expectedMessage);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(true, "Exception message")]
    [InlineData(false, "Exception message")]
    public void BadRequestException_When_Meets_Condition(bool condition, string message)
    {
        if (condition)
        {
            Assert.Throws<DbRegisterExistsException>(() => { DbRegisterExistsException.ThrowIf(condition, message); });
        }
        else
        {
            DbRegisterExistsException.ThrowIf(condition, message);
            Assert.True(true);
        }
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
        Assert.Equal(expectedMessage, e.Message);
    }
}
