namespace Fintech.IdentityService.Application.Auth.RefreshAccessToken;

public sealed record RefreshAccessTokenResult(
    string AccessToken,
    DateTime AccessTokenExpiresAtUtc,
    string RefreshToken,
    DateTime RefreshTokenExpiresAtUtc,
    Guid UserId,
    string Email);
