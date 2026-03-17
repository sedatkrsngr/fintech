namespace Fintech.IdentityService.Configuration;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; set; } = "fintech.identity";

    public string Audience { get; set; } = "fintech.gateway";

    public string SigningKey { get; set; } = "super-secret-dev-signing-key-change-me";

    public int AccessTokenExpirationMinutes { get; set; } = 60;
}
