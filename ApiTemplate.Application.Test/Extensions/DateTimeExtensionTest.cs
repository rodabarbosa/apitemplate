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

        result
            .Should()
            .Be(new DateTime(2019, 12, 25));
    }
}
