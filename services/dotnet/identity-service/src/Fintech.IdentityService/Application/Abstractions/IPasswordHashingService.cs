namespace Fintech.IdentityService.Application.Abstractions;

public interface IPasswordHashingService
{
    string HashPassword(string password);

    bool VerifyPassword(string hashedPassword, string providedPassword);
}
