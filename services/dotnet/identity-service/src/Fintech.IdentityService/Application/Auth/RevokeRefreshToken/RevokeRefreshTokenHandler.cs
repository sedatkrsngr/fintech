using Fintech.IdentityService.Application.Abstractions;

namespace Fintech.IdentityService.Application.Auth.RevokeRefreshToken;

public sealed class RevokeRefreshTokenHandler
{
    private readonly IAuthLifecycleRepository _authLifecycleRepository;
    private readonly IPasswordHashingService _passwordHashingService;

    public RevokeRefreshTokenHandler(
        IAuthLifecycleRepository authLifecycleRepository,
        IPasswordHashingService passwordHashingService)
    {
        _authLifecycleRepository = authLifecycleRepository;
        _passwordHashingService = passwordHashingService;
    }

    public async Task<bool> HandleAsync(
        RevokeRefreshTokenCommand command,
        CancellationToken cancellationToken = default)
    {
        if (!AuthTokenFormat.TryParseRefreshToken(command.RefreshToken, out var refreshTokenId, out var secret))
        {
            return false;
        }

        var refreshToken = await _authLifecycleRepository.GetRefreshTokenByIdAsync(refreshTokenId, cancellationToken);
        if (refreshToken is null)
        {
            return false;
        }

        var tokenValid = _passwordHashingService.VerifyPassword(refreshToken.HashedToken.Value, secret);
        if (!tokenValid)
        {
            return false;
        }

        refreshToken.Revoke();
        await _authLifecycleRepository.UpdateRefreshTokenAsync(refreshToken, cancellationToken);
        return true;
    }
}
