using ApiTemplate.Shared.Exceptions;
using System.Runtime.Serialization.Formatters.Binary;

namespace ApiTemplate.Shared.Test.Exceptions;

public class SaveFailureExceptionTest
{
    [Fact]
    public void SaveFailureException_Should_Be_Created()
    {
        // Act
        var exception = new SaveFailureException();

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void SaveFailureException_Should_Have_Correct_Inner_Exception()
    {
        // Arrange
        var expectedInnerException = new Exception();

        // Act
        var exception = new SaveFailureException(expectedInnerException);

        // Assert
        Assert.Equal(expectedInnerException, exception.InnerException);
    }

    [Fact]
    public void SaveFailureException_Should_Have_Correct_Message()
    {
        // Arrange
        var expectedMessage = "Save has failed";

        // Act
        var exception = new SaveFailureException(expectedMessage);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(true, "Exception message")]
    [InlineData(false, "Exception message")]
    public void SaveFailureException_When_Meets_Condition(bool condition, string message)
    {
        if (condition)
        {
            Assert.Throws<SaveFailureException>(() => { SaveFailureException.ThrowIf(condition, message); });
        }
        else
        {
            SaveFailureException.ThrowIf(condition, message);
            Assert.True(true);
        }
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
        Assert.Equal(expectedMessage, e.Message);
    }
}
