namespace Fintech.IdentityService.Api.Requests;

public sealed record CreateApiClientRequest(
    string Name,
    string[] AllowedIps);
