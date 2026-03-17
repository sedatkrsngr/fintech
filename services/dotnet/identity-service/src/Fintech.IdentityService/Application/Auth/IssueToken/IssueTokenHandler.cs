using System.IdentityModel.Tokens.Jwt;
using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Configuration;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace Fintech.IdentityService.Application.Auth.IssueToken;

public sealed class IssueTokenHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly IAuthLifecycleRepository _authLifecycleRepository;
    private readonly ITokenSecretService _tokenSecretService;
    private readonly ITokenService _tokenService;
    private readonly AuthLifecycleOptions _authLifecycleOptions;

    public IssueTokenHandler(
        IUserRepository userRepository,
        IPasswordHashingService passwordHashingService,
        IAuthLifecycleRepository authLifecycleRepository,
        ITokenSecretService tokenSecretService,
        ITokenService tokenService,
        IOptions<AuthLifecycleOptions> authLifecycleOptions)
    {
        _userRepository = userRepository;
        _passwordHashingService = passwordHashingService;
        _authLifecycleRepository = authLifecycleRepository;
        _tokenSecretService = tokenSecretService;
        _tokenService = tokenService;
        _authLifecycleOptions = authLifecycleOptions.Value;
    }

    public async Task<IssueTokenResult?> HandleAsync(
        IssueTokenCommand command,
        CancellationToken cancellationToken = default)
    {
        var email = Email.Create(command.Email);
        var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

        if (user is null)
        {
            return null;
        }

        var passwordVerified = _passwordHashingService.VerifyPassword(
            user.PasswordHash.Value,
            command.Password);

        if (!passwordVerified)
        {
            return null;
        }

        var accessToken = _tokenService.CreateAccessToken(user);
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
        var refreshTokenSecret = _tokenSecretService.GenerateSecret();
        var refreshToken = RefreshToken.Issue(
            user.Id,
            HashedRefreshToken.Create(_passwordHashingService.HashPassword(refreshTokenSecret)),
            DateTime.UtcNow.AddDays(_authLifecycleOptions.RefreshTokenExpirationDays));

        await _authLifecycleRepository.AddRefreshTokenAsync(refreshToken, cancellationToken);

        return new IssueTokenResult(
            accessToken,
            jwtToken.ValidTo,
            AuthTokenFormat.BuildRefreshToken(refreshToken.Id, refreshTokenSecret),
            refreshToken.ExpiresAtUtc,
            user.Id.Value,
            user.Email.Value);
    }
}
