using Fintech.IdentityService.Domain.Entities;

namespace Fintech.IdentityService.Application.Abstractions;

public interface ITokenService
{
    string CreateAccessToken(User user);
}
