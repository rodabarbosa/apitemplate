using ApiTemplate.Domain.Entities;

namespace ApiTemplate.Infra.Data.Seeds;

public static class UserSeed
{
    public static IEnumerable<User> GetSeeds()
    {
        return new List<User>
        {
            new()
            {
                Id = new Guid("afaf98be-62da-4975-9dd0-e9c50c871236"),
                Name = "Test User",
                Email = "admin@tester.com",
                Username = "admin",
                Password = "admin@123",
                Role = "Tester",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0),
                Active = true
            }
        };
    }
}
