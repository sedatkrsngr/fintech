namespace Fintech.IdentityService.Application.ApiAccess.CreateApiClient;

public sealed record CreateApiClientResult(
    Guid ApiClientId,
    string Name,
    string ApiKey,
    DateTime CreatedAtUtc,
    IReadOnlyList<string> AllowedIps);
