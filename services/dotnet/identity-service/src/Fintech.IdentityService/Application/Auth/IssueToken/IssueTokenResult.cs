namespace Fintech.IdentityService.Application.Auth.IssueToken;

public sealed record IssueTokenResult(
    string AccessToken,
    DateTime ExpiresAtUtc,
    Guid UserId,
    string Email);
