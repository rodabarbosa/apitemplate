using ApiTemplate.Domain.Entities;

namespace ApiTemplate.Domain.Test.Entities;

public class UserTest
{
    [Fact]
    public void User_CanBeCreated()
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test User",
            Email = "user@test.com",
            Username = "test",
            Password = "test@123",
            Role = "Guest",
            CreatedAt = DateTime.Now,
            Active = true
        };

        Assert.NotNull(user);
    }
}
