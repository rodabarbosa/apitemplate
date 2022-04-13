namespace ApiTemplate.WebApi.Jwt.Interfaces;

public interface ITokenConfiguration
{
    string Audience { get; }
    string Issuer { get; }
    int Seconds { get; }
}
