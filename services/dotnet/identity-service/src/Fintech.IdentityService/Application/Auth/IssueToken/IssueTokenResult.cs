namespace Fintech.IdentityService.Application.Auth.IssueToken;

public sealed record IssueTokenResult(
    string AccessToken,
    DateTime AccessTokenExpiresAtUtc,
    string RefreshToken,
    DateTime RefreshTokenExpiresAtUtc,
    Guid UserId,
    string Email);
