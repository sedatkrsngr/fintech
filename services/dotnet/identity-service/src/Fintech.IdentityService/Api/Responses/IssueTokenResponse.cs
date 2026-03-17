namespace Fintech.IdentityService.Api.Responses;

public sealed record IssueTokenResponse(
    string AccessToken,
    DateTime ExpiresAtUtc,
    Guid UserId,
    string Email);
