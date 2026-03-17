using System.IdentityModel.Tokens.Jwt;
using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Configuration;
using Fintech.IdentityService.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace Fintech.IdentityService.Application.Auth.IssueToken;

public sealed class IssueTokenHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly ITokenService _tokenService;
    private readonly JwtOptions _jwtOptions;

    public IssueTokenHandler(
        IUserRepository userRepository,
        IPasswordHashingService passwordHashingService,
        ITokenService tokenService,
        IOptions<JwtOptions> jwtOptions)
    {
        _userRepository = userRepository;
        _passwordHashingService = passwordHashingService;
        _tokenService = tokenService;
        _jwtOptions = jwtOptions.Value;
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
        var expiresAtUtc = jwtToken.ValidTo;

        return new IssueTokenResult(
            accessToken,
            expiresAtUtc,
            user.Id.Value,
            user.Email.Value);
    }
}
