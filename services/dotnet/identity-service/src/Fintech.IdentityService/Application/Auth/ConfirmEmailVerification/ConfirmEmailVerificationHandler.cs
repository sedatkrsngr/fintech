using Fintech.IdentityService.Application.Abstractions;

namespace Fintech.IdentityService.Application.Auth.ConfirmEmailVerification;

public sealed class ConfirmEmailVerificationHandler
{
    private readonly IAuthLifecycleRepository _authLifecycleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashingService _passwordHashingService;

    public ConfirmEmailVerificationHandler(
        IAuthLifecycleRepository authLifecycleRepository,
        IUserRepository userRepository,
        IPasswordHashingService passwordHashingService)
    {
        _authLifecycleRepository = authLifecycleRepository;
        _userRepository = userRepository;
        _passwordHashingService = passwordHashingService;
    }

    public async Task<bool> HandleAsync(
        ConfirmEmailVerificationCommand command,
        CancellationToken cancellationToken = default)
    {
        if (!AuthTokenFormat.TryParseEmailVerificationToken(command.Token, out var tokenId, out var secret))
        {
            return false;
        }

        var token = await _authLifecycleRepository.GetEmailVerificationTokenByIdAsync(tokenId, cancellationToken);
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

        user.MarkEmailVerified();
        token.Consume();

        await _userRepository.UpdateAsync(user, cancellationToken);
        await _authLifecycleRepository.UpdateEmailVerificationTokenAsync(token, cancellationToken);

        return true;
    }
}
