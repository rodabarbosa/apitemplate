using System.Security.Cryptography;
using ApiTemplate.Application.Jwt.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ApiTemplate.Application.Jwt.Models;

/// <inheritdoc />
public class SigningConfiguration : ISigningConfiguration
{
    /// <inheritdoc />
    public SecurityKey Key { get; }

    /// <inheritdoc />
    public SigningCredentials SigningCredentials { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SigningConfiguration"/> class.
    /// </summary>
    public SigningConfiguration()
    {
        using (var provider = new RSACryptoServiceProvider(2048))
        {
            Key = new RsaSecurityKey(provider.ExportParameters(true));
        }

        SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
    }
}