using System;
using ApiTemplate.Application.Extensions;
using Xunit;

namespace ApiTemplate.Tests.ApplicationProjectTest.Extensions;

public class DateTimeExtensionTest
{
    [Fact]
    public void ToDateTime_ShouldReturnDateTime()
    {
        // Arrange
        string dateTime = "2019-12-25T00:00:00";

        // Act
        DateTime result = dateTime.ToDateTime();

        // Assert
        Assert.Equal(new DateTime(2019, 12, 25), result);
    }
}