namespace Fintech.IdentityService.Application.ApiAccess.ValidateApiKey;

public sealed record ValidateApiKeyResult(
    Guid ApiClientId,
    string Name);
