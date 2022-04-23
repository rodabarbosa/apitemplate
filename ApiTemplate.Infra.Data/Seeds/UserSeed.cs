using ApiTemplate.Domain.Entities;

namespace ApiTemplate.Infra.Data.Seeds;

public static class UserSeed
{
    public static List<User> GetSeeds()
    {
        return new List<User>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Email = "admin@tester.com",
                Username = "admin",
                Password = "admin@123",
                Role = "Tester",
                CreatedAt = DateTime.Now,
                Active = true
            }
        };
    }
}
