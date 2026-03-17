namespace Fintech.ApiGateway.Infrastructure.Auth;

public sealed record ValidateApiKeyResponse(
    Guid ApiClientId,
    string Name);
