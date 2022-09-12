using System.Threading.Tasks;

namespace ApiTemplate.Domain.Repositories;

public interface IUserRepository
{
    bool IsUserValid(string username, string password);

    Task<bool> IsUserValidAsync(string username, string password);
}
