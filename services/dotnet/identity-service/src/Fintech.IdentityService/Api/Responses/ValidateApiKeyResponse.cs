namespace Fintech.IdentityService.Api.Responses;

public sealed record ValidateApiKeyResponse(
    Guid ApiClientId,
    string Name);
