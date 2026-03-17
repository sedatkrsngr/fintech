namespace Fintech.IdentityService.Api.Responses;

public sealed record CreateApiClientResponse(
    Guid ApiClientId,
    string Name,
    string ApiKey,
    DateTime CreatedAtUtc,
    IReadOnlyList<string> AllowedIps);
