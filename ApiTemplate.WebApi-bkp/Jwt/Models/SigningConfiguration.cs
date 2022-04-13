using System.Security.Cryptography;
using ApiTemplate.WebApi.Jwt.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ApiTemplate.WebApi.Jwt.Models;

public class SigningConfiguration : ISigningConfiguration
{
    public SecurityKey Key { get; }
    public SigningCredentials SigningCredentials { get; }

    public SigningConfiguration()
    {
        using (var provider = new RSACryptoServiceProvider(2048))
        {
            Key = new RsaSecurityKey(provider.ExportParameters(true));
        }

        SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
    }
}
