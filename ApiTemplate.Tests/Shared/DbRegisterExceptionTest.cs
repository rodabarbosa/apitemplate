using System;
using ApiTemplate.Shared.Exceptions;
using Xunit;

namespace ApiTemplate.Tests.Shared;

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
            Assert.Throws<DbRegisterExistsException>(() => { DbRegisterExistsException.When(condition, message); });
        }
        else
        {
            DbRegisterExistsException.When(condition, message);
            Assert.True(true);
        }
    }
}
