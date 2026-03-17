using System.IdentityModel.Tokens.Jwt;
using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Configuration;
using Microsoft.Extensions.Options;

namespace Fintech.IdentityService.Application.Auth.RefreshAccessToken;

public sealed class RefreshAccessTokenHandler
{
    private readonly IAuthLifecycleRepository _authLifecycleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly ITokenSecretService _tokenSecretService;
    private readonly ITokenService _tokenService;
    private readonly AuthLifecycleOptions _authLifecycleOptions;

    public RefreshAccessTokenHandler(
        IAuthLifecycleRepository authLifecycleRepository,
        IUserRepository userRepository,
        IPasswordHashingService passwordHashingService,
        ITokenSecretService tokenSecretService,
        ITokenService tokenService,
        IOptions<AuthLifecycleOptions> authLifecycleOptions)
    {
        _authLifecycleRepository = authLifecycleRepository;
        _userRepository = userRepository;
        _passwordHashingService = passwordHashingService;
        _tokenSecretService = tokenSecretService;
        _tokenService = tokenService;
        _authLifecycleOptions = authLifecycleOptions.Value;
    }

    public async Task<RefreshAccessTokenResult?> HandleAsync(
        RefreshAccessTokenCommand command,
        CancellationToken cancellationToken = default)
    {
        if (!AuthTokenFormat.TryParseRefreshToken(command.RefreshToken, out var refreshTokenId, out var secret))
        {
            return null;
        }

        var refreshToken = await _authLifecycleRepository.GetRefreshTokenByIdAsync(refreshTokenId, cancellationToken);
        if (refreshToken is null || !refreshToken.IsActiveAt(DateTime.UtcNow))
        {
            return null;
        }

        var tokenValid = _passwordHashingService.VerifyPassword(refreshToken.HashedToken.Value, secret);
        if (!tokenValid)
        {
            return null;
        }

        var user = await _userRepository.GetByIdAsync(refreshToken.UserId, cancellationToken);
        if (user is null)
        {
            return null;
        }

        var newSecret = _tokenSecretService.GenerateSecret();
        var newRefreshToken = Domain.Entities.RefreshToken.Issue(
            user.Id,
            Domain.ValueObjects.HashedRefreshToken.Create(_passwordHashingService.HashPassword(newSecret)),
            DateTime.UtcNow.AddDays(_authLifecycleOptions.RefreshTokenExpirationDays));

        refreshToken.Revoke(newRefreshToken.Id);

        await _authLifecycleRepository.UpdateRefreshTokenAsync(refreshToken, cancellationToken);
        await _authLifecycleRepository.AddRefreshTokenAsync(newRefreshToken, cancellationToken);

        var accessToken = _tokenService.CreateAccessToken(user);
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);

        return new RefreshAccessTokenResult(
            accessToken,
            jwtToken.ValidTo,
            AuthTokenFormat.BuildRefreshToken(newRefreshToken.Id, newSecret),
            newRefreshToken.ExpiresAtUtc,
            user.Id.Value,
            user.Email.Value);
    }
}
