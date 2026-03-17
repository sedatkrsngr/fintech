using Fintech.IdentityService.Application.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Fintech.IdentityService.Infrastructure.Security;

public sealed class PasswordHashingService : IPasswordHashingService
{
    private readonly PasswordHasher<object> _passwordHasher = new();

    public string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password is required.", nameof(password));
        }

        return _passwordHasher.HashPassword(new object(), password);
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword))
        {
            throw new ArgumentException("Hashed password is required.", nameof(hashedPassword));
        }

        if (string.IsNullOrWhiteSpace(providedPassword))
        {
            throw new ArgumentException("Provided password is required.", nameof(providedPassword));
        }

        var result = _passwordHasher.VerifyHashedPassword(new object(), hashedPassword, providedPassword);

        return result != PasswordVerificationResult.Failed;
    }
}
