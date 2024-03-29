﻿namespace ApiTemplate.Shared.Test.Exceptions;

public class BadRequestExceptionTest
{
    [Fact]
    public void BadRequestException_Should_Be_Created()
    {
        // Act
        var exception = new BadRequestException();

        // Assert
        exception.Should()
            .NotBeNull();
    }

    [Fact]
    public void BadRequestException_Should_Be_Created_With_InnerException()
    {
        // Act
        var innerException = new Exception();
        var exception = new BadRequestException(innerException);

        // Assert
        exception.Should()
            .NotBeNull();

        exception.InnerException
            .Should()
            .NotBeNull();
    }

    [Fact]
    public void BadRequestException_Should_Have_Correct_Inner_Exception()
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
    public void BadRequestException_Should_Have_Correct_Message()
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
    public void Should_Throw_BadRequestException_When_Meet_Condition(bool condition, string message)
    {
        var act = () => BadRequestException.ThrowIf(condition, message);

        act.Should()
            .Throw<BadRequestException>();
    }

    [Theory]
    [InlineData(false, "Exception message")]
    public void Should_Not_Throw_BadRequestException_When_Meet_Condition(bool condition, string message)
    {
        var act = () => BadRequestException.ThrowIf(condition, message);

        act.Should()
            .NotThrow();
    }
}
