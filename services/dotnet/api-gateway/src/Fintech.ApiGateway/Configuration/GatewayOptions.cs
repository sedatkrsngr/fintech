namespace Fintech.ApiGateway.Configuration;

public sealed class GatewayOptions
{
    public const string SectionName = "Gateway";

    public string ServiceName { get; init; } = "api-gateway";

    public string BasePath { get; init; } = "/";
}
