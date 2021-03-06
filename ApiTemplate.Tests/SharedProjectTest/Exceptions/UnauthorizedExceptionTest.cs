using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ApiTemplate.Shared.Exceptions;
using Xunit;

namespace ApiTemplate.Tests.SharedProjectTest.Exceptions;

public class UnathorizedExceptionTest
{
    [Fact]
    public void UnathorizedException_Should_Be_Created()
    {
        // Act
        var exception = new UnauthorizedException();

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void UnathorizedException_Should_Have_Correct_Inner_Exception()
    {
        // Arrange
        var expectedInnerException = new Exception();

        // Act
        var exception = new UnauthorizedException(expectedInnerException);

        // Assert
        Assert.Equal(expectedInnerException.Message, exception.InnerException!.Message);
    }

    [Fact]
    public void UnathorizedException_Should_Have_Correct_Message()
    {
        // Arrange
        var expectedMessage = "Unauthorized Access";

        // Act
        var exception = new UnauthorizedException(expectedMessage);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(true, "Exception message")]
    [InlineData(false, "Exception message")]
    public void UnathorizedException_When_Meets_Condition(bool condition, string message)
    {
        if (condition)
        {
            Assert.Throws<UnauthorizedException>(() => { UnauthorizedException.ThrowIf(condition, message); });
        }
        else
        {
            UnauthorizedException.ThrowIf(condition, message);
            Assert.True(true);
        }
    }

    [Fact]
    public void UnauthorizedException_Serialization()
    {
        // Arrange
        var expectedMessage = "Serialization test";
        var e = new UnauthorizedException(expectedMessage);

        // Act
        using (Stream s = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011
            formatter.Serialize(s, e);
            s.Position = 0; // Reset stream position
            e = (UnauthorizedException)formatter.Deserialize(s);
#pragma warning restore SYSLIB0011
        }

        // Assert
        Assert.Equal(expectedMessage, e.Message);
    }
}
