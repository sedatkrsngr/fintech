using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.Auth.ConfirmPasswordReset;

public sealed class ConfirmPasswordResetHandler
{
    private readonly IAuthLifecycleRepository _authLifecycleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashingService _passwordHashingService;

    public ConfirmPasswordResetHandler(
        IAuthLifecycleRepository authLifecycleRepository,
        IUserRepository userRepository,
        IPasswordHashingService passwordHashingService)
    {
        _authLifecycleRepository = authLifecycleRepository;
        _userRepository = userRepository;
        _passwordHashingService = passwordHashingService;
    }

    public async Task<bool> HandleAsync(
        ConfirmPasswordResetCommand command,
        CancellationToken cancellationToken = default)
    {
        if (!AuthTokenFormat.TryParsePasswordResetToken(command.Token, out var tokenId, out var secret))
        {
            return false;
        }

        var token = await _authLifecycleRepository.GetPasswordResetTokenByIdAsync(tokenId, cancellationToken);
        if (token is null || !token.IsActiveAt(DateTime.UtcNow))
        {
            return false;
        }

        var tokenValid = _passwordHashingService.VerifyPassword(token.HashedToken.Value, secret);
        if (!tokenValid)
        {
            return false;
        }

        var user = await _userRepository.GetByIdAsync(token.UserId, cancellationToken);
        if (user is null)
        {
            return false;
        }

        user.ChangePasswordHash(PasswordHash.Create(_passwordHashingService.HashPassword(command.NewPassword)));
        token.Consume();

        await _userRepository.UpdateAsync(user, cancellationToken);
        await _authLifecycleRepository.UpdatePasswordResetTokenAsync(token, cancellationToken);

        return true;
    }
}
