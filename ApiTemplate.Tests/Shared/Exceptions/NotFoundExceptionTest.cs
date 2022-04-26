using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ApiTemplate.Shared.Exceptions;
using Xunit;

namespace ApiTemplate.Tests.Shared.Exceptions;

public class NotFoundExceptionTest
{
    [Fact]
    public void NotFoundException_Should_Be_Created()
    {
        // Act
        var exception = new NotFoundException();

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void NotFoundException_Should_Have_Correct_Inner_Exception()
    {
        // Arrange
        var expectedInnerException = new Exception();

        // Act
        var exception = new NotFoundException(expectedInnerException);

        // Assert
        Assert.Equal(expectedInnerException, exception.InnerException);
    }

    [Fact]
    public void NotFoundException_Should_Have_Correct_Message()
    {
        // Arrange
        var expectedMessage = "Not Found Exception";

        // Act
        var exception = new NotFoundException(expectedMessage);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(true, "Exception message")]
    [InlineData(false, "Exception message")]
    public void NotFoundException_When_Meets_Condition(bool condition, string message)
    {
        if (condition)
        {
            Assert.Throws<NotFoundException>(() => { NotFoundException.When(condition, message); });
        }
        else
        {
            NotFoundException.When(condition, message);
            Assert.True(true);
        }
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
            formatter.Serialize(s, e);
            s.Position = 0; // Reset stream position
            e = (NotFoundException) formatter.Deserialize(s);
        }

        // Assert
        Assert.Equal(expectedMessage, e.Message);
    }
}
