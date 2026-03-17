namespace Fintech.ApiGateway.Configuration;

public sealed class InternalAuthOptions
{
    public const string SectionName = "InternalAuth";

    public string IdentityBaseUrl { get; set; } = "http://localhost:5001";
}
