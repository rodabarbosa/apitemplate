using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ApiTemplate.Shared.Exceptions;
using Xunit;

namespace ApiTemplate.Tests.SharedProjectTest.Exceptions;

public class ForbiddenExceptionTest
{
    [Fact]
    public void ForbiddenException_Should_Be_Created()
    {
        // Act
        var exception = new ForbiddenException();

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void ForbiddenException_Should_Have_Correct_Inner_Exception()
    {
        // Arrange
        var expectedInnerException = new Exception();

        // Act
        var exception = new ForbiddenException(expectedInnerException);

        // Assert
        Assert.Equal(expectedInnerException, exception.InnerException);
    }

    [Fact]
    public void ForbiddenException_Should_Have_Correct_Message()
    {
        // Arrange
        var expectedMessage = "Access Forbidden";

        // Act
        var exception = new ForbiddenException(expectedMessage);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(true, "Exception message")]
    [InlineData(false, "Exception message")]
    public void ForbiddenException_When_Meets_Condition(bool condition, string message)
    {
        if (condition)
        {
            Assert.Throws<ForbiddenException>(() => { ForbiddenException.ThrowIf(condition, message); });
        }
        else
        {
            ForbiddenException.ThrowIf(condition, message);
            Assert.True(true);
        }
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
            e = (ForbiddenException) formatter.Deserialize(s);
#pragma warning restore SYSLIB0011
        }

        // Assert
        Assert.Equal(expectedMessage, e.Message);
    }
}
