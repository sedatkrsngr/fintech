namespace Fintech.IdentityService.Api.Responses;

public sealed record TokenEnvelopeResponse(
    string AccessToken,
    DateTime AccessTokenExpiresAtUtc,
    string RefreshToken,
    DateTime RefreshTokenExpiresAtUtc,
    Guid UserId,
    string Email);
