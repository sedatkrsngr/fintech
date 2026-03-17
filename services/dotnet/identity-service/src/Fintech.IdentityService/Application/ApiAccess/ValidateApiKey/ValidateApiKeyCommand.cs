namespace Fintech.IdentityService.Application.ApiAccess.ValidateApiKey;

public sealed record ValidateApiKeyCommand(
    string ApiKey,
    string? RemoteIp);
