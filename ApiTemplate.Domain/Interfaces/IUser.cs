namespace ApiTemplate.Domain.Interfaces;

public interface IUser
{
    string Id { get; set; }
    string UserName { get; set; }
    string Email { get; set; }
}
