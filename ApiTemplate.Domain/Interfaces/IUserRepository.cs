using System.Threading.Tasks;

namespace ApiTemplate.Domain.Interfaces;

public interface IUserRepository
{
    bool IsUserValid(string username, string password);

    Task<bool> IsUserValidAsync(string username, string password);
}