namespace Fintech.IdentityService.Application.ApiAccess.CreateApiClient;

public sealed record CreateApiClientCommand(
    string Name,
    string[] AllowedIps);
