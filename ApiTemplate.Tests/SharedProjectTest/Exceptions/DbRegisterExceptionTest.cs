using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ApiTemplate.Shared.Exceptions;
using Xunit;

namespace ApiTemplate.Tests.SharedProjectTest.Exceptions;

public class DbRegisterExceptionTest
{
    [Fact]
    public void DbRegisterException_Should_Be_Created()
    {
        // Act
        var exception = new DbRegisterExistsException();

        // Assert
        Assert.NotNull(exception);
    }

    [Fact]
    public void DbRegisterException_Should_Have_Correct_Inner_Exception()
    {
        // Arrange
        var expectedInnerException = new Exception();

        // Act
        var exception = new DbRegisterExistsException(expectedInnerException);

        // Assert
        Assert.Equal(expectedInnerException, exception.InnerException);
    }

    [Fact]
    public void DbRegisterException_Should_Have_Correct_Message()
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
    public void DbRegisterException_When_Meets_Condition(bool condition, string message)
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
