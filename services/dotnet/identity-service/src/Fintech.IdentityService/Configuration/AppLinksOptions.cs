namespace Fintech.IdentityService.Configuration;

public sealed class AppLinksOptions
{
    public const string SectionName = "AppLinks";

    public string ResetPasswordBaseUrl { get; init; } = "http://localhost:3000/reset-password";

    public string EmailVerificationBaseUrl { get; init; } = "http://localhost:3000/verify-email";
}
