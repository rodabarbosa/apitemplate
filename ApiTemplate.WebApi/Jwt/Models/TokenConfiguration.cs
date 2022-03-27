using ApiTemplate.WebApi.Jwt.Interfaces;

namespace ApiTemplate.WebApi.Jwt.Models;

public class TokenConfiguration : ITokenConfiguration
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int Seconds { get; set; }
}
