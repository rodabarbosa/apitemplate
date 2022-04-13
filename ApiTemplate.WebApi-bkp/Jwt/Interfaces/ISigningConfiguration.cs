using Microsoft.IdentityModel.Tokens;

namespace ApiTemplate.WebApi.Jwt.Interfaces;

public interface ISigningConfiguration
{
    SecurityKey Key { get; }
    SigningCredentials SigningCredentials { get; }
}
