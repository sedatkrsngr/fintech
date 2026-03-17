using System.Security.Cryptography;
using Fintech.IdentityService.Application.Abstractions;

namespace Fintech.IdentityService.Infrastructure.Security;

public sealed class TokenSecretService : ITokenSecretService
{
    public string GenerateSecret()
    {
        var bytes = RandomNumberGenerator.GetBytes(48);
        return Convert.ToBase64String(bytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .TrimEnd('=');
    }
}
