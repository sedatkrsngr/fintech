namespace Fintech.IdentityService.Configuration;

public sealed class AuthLifecycleOptions
{
    public const string SectionName = "AuthLifecycle";

    public int RefreshTokenExpirationDays { get; init; } = 30;

    public int PasswordResetTokenExpirationMinutes { get; init; } = 30;

    public int EmailVerificationTokenExpirationHours { get; init; } = 24;
}
