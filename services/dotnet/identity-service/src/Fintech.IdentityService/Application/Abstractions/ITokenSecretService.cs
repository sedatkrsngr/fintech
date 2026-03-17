namespace Fintech.IdentityService.Application.Abstractions;

public interface ITokenSecretService
{
    string GenerateSecret();
}
