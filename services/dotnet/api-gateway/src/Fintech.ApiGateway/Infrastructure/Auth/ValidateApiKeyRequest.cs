namespace Fintech.ApiGateway.Infrastructure.Auth;

public sealed record ValidateApiKeyRequest(
    string ApiKey,
    string? RemoteIp);
