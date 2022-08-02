using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ApiTemplate.Shared.Exceptions;
using Xunit;

namespace ApiTemplate.Tests.SharedProjectTest.Exceptions;

public class DeleteFailureExceptionTest
{
    [Fact]
    public void DeleteFailureException_Should_Be_Created()
    {
        // Act
        var exception = new DeleteFailureException();

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void DeleteFailureException_Should_Have_Correct_Inner_Exception()
    {
        // Arrange
        var expectedInnerException = new Exception();

        // Act
        var exception = new DeleteFailureException(expectedInnerException);

        // Assert
        Assert.Equal(expectedInnerException, exception.InnerException);
    }

    [Fact]
    public void DeleteFailureException_Should_Have_Correct_Message()
    {
        // Arrange
        var expectedMessage = "Delete has failed";

        // Act
        var exception = new DeleteFailureException(expectedMessage);

        // Assert
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(true, "Exception message")]
    [InlineData(false, "Exception message")]
    public void DeleteFailureException_When_Meets_Condition(bool condition, string message)
    {
        if (condition)
        {
            Assert.Throws<DeleteFailureException>(() => { DeleteFailureException.ThrowIf(condition, message); });
        }
        else
        {
            DeleteFailureException.ThrowIf(condition, message);
            Assert.True(true);
        }
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
        Assert.Equal(expectedMessage, e.Message);
    }
}
