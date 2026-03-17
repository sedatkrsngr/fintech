using Fintech.Contracts.Notifications;
using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Configuration;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace Fintech.IdentityService.Application.Auth.RequestEmailVerification;

public sealed class RequestEmailVerificationHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthLifecycleRepository _authLifecycleRepository;
    private readonly INotificationDispatchClient _notificationDispatchClient;
    private readonly IPasswordHashingService _passwordHashingService;
    private readonly ITokenSecretService _tokenSecretService;
    private readonly AuthLifecycleOptions _authLifecycleOptions;
    private readonly AppLinksOptions _appLinksOptions;

    public RequestEmailVerificationHandler(
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

    public async Task<RequestEmailVerificationResult> HandleAsync(
        RequestEmailVerificationCommand command,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsync(Email.Create(command.Email), cancellationToken);
        if (user is null)
        {
            return new RequestEmailVerificationResult(true);
        }

        var secret = _tokenSecretService.GenerateSecret();
        var token = EmailVerificationToken.Issue(
            user.Id,
            HashedEmailVerificationToken.Create(_passwordHashingService.HashPassword(secret)),
            DateTime.UtcNow.AddHours(_authLifecycleOptions.EmailVerificationTokenExpirationHours));

        var plainToken = AuthTokenFormat.BuildEmailVerificationToken(token.Id, secret);
        var verificationLink = AuthLinkBuilder.Build(_appLinksOptions.EmailVerificationBaseUrl, plainToken);
        var subject = "Email verification";
        var body =
            "Use this link to verify your email address:" + Environment.NewLine +
            verificationLink + Environment.NewLine + Environment.NewLine +
            "Expires at (UTC): " + token.ExpiresAtUtc.ToString("O");

        await _notificationDispatchClient.SendEmailAsync(
            NotificationMessageType.EmailVerificationRequested,
            user.Email.Value,
            subject,
            body,
            cancellationToken);

        await _authLifecycleRepository.AddEmailVerificationTokenAsync(token, cancellationToken);

        return new RequestEmailVerificationResult(true);
    }
}
