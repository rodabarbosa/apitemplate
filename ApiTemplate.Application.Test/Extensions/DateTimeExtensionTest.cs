using ApiTemplate.Application.Extensions;

namespace ApiTemplate.Application.Test.Extensions;

public class DateTimeExtensionTest
{
    [Fact]
    public void ToDateTime_ShouldReturnDateTime()
    {
        // Arrange
        var dateTime = "2019-12-25T00:00:00";

        // Act
        var result = dateTime.ToDateTime();

        // Assert
        Assert.Equal(new DateTime(2019, 12, 25), result);
    }
}
