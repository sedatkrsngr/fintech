namespace Fintech.IdentityService.Api.Requests;

public sealed record ValidateApiKeyRequest(
    string ApiKey,
    string? RemoteIp);
