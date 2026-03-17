namespace Fintech.ApiGateway.Configuration;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Issuer { get; set; } = "fintech.identity.dev";

    public string Audience { get; set; } = "fintech.gateway";

    public string SigningKey { get; set; } = "super-secret-dev-signing-key-change-me";
}
