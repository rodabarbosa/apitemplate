using ApiTemplate.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Infra.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApiTemplateContext _context;

    public UserRepository(ApiTemplateContext context)
    {
        _context = context;
    }

    public bool IsUserValid(string username, string password)
    {
        return _context.Users.Any(x => x.Active && x.Username == username && x.Password == password);
    }

    public async Task<bool> IsUserValidAsync(string username, string password)
    {
        return await _context.Users.AnyAsync(x => x.Active && x.Username == username && x.Password == password);
    }
}
