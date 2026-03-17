using Fintech.Contracts.Notifications;
using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Configuration;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace Fintech.IdentityService.Application.Auth.RequestPasswordReset;

public sealed class RequestPasswordResetHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthLifecycleRepository _authLifecycleRepository;
    private readonly INotificationDispatchClient _notificationDispatchClient;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly ITokenSecretService _tokenSecretService;
    private readonly AuthLifecycleOptions _authLifecycleOptions;
    private readonly AppLinksOptions _appLinksOptions;

    public RequestPasswordResetHandler(
        IUserRepository userRepository,
        IAuthLifecycleRepository authLifecycleRepository,
        INotificationDispatchClient notificationDispatchClient,
        IPasswordHashingService passwordHashingService,
        ITokenSecretService tokenSecretService,
        IOptions<AuthLifecycleOptions> authLifecycleOptions,
        IOptions<AppLinksOptions> appLinksOptions)
    {
        _userRepository = userRepository;
        _authLifecycleRepository = authLifecycleRepository;
        _notificationDispatchClient = notificationDispatchClient;
        _passwordHashingService = passwordHashingService;
        _tokenSecretService = tokenSecretService;
        _authLifecycleOptions = authLifecycleOptions.Value;
        _appLinksOptions = appLinksOptions.Value;
    }

    public async Task<RequestPasswordResetResult> HandleAsync(
        RequestPasswordResetCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsync(Email.Create(command.Email), cancellationToken);
        if (user is null)
        {
            return new RequestPasswordResetResult(true);
        }

        var secret = _tokenSecretService.GenerateSecret();
        var token = PasswordResetToken.Issue(
            user.Id,
            HashedPasswordResetToken.Create(_passwordHashingService.HashPassword(secret)),
            DateTime.UtcNow.AddMinutes(_authLifecycleOptions.PasswordResetTokenExpirationMinutes));

        var plainToken = AuthTokenFormat.BuildPasswordResetToken(token.Id, secret);
        var resetLink = AuthLinkBuilder.Build(_appLinksOptions.ResetPasswordBaseUrl, plainToken);
        var subject = "Password reset request";
        var body =
            "Use this link to reset your password:" + Environment.NewLine +
            resetLink + Environment.NewLine + Environment.NewLine +
            "Expires at (UTC): " + token.ExpiresAtUtc.ToString("O");

        await _notificationDispatchClient.SendEmailAsync(
            NotificationMessageType.PasswordResetRequested,
            user.Email.Value,
            subject,
            body,
            cancellationToken);

        await _authLifecycleRepository.AddPasswordResetTokenAsync(token, cancellationToken);

        return new RequestPasswordResetResult(true);
    }
}
