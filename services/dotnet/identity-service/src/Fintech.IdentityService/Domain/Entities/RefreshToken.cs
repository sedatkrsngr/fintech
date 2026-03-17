using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Domain.Entities;

public sealed class RefreshToken
{
    private RefreshToken(
        RefreshTokenId id,
        UserId userId,
        HashedRefreshToken hashedToken,
        DateTime expiresAtUtc)
    {
        if (expiresAtUtc <= DateTime.UtcNow)
        {
            throw new ArgumentException("Refresh token must expire in the future.", nameof(expiresAtUtc));
        }

        Id = id;
        UserId = userId;
        HashedToken = hashedToken;
        ExpiresAtUtc = expiresAtUtc;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public RefreshTokenId Id { get; }

    public UserId UserId { get; }

    public HashedRefreshToken HashedToken { get; private set; }

    public DateTime ExpiresAtUtc { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public DateTime? RevokedAtUtc { get; private set; }

    public RefreshTokenId? ReplacedByTokenId { get; private set; }

    public static RefreshToken Issue(
        UserId userId,
        HashedRefreshToken hashedToken,
        DateTime expiresAtUtc)
    {
        return new RefreshToken(RefreshTokenId.New(), userId, hashedToken, expiresAtUtc);
    }

    public bool IsActiveAt(DateTime utcNow)
    {
        return RevokedAtUtc is null && ExpiresAtUtc > utcNow;
    }

    public void Revoke(RefreshTokenId? replacedByTokenId = null)
    {
        RevokedAtUtc ??= DateTime.UtcNow;
        ReplacedByTokenId ??= replacedByTokenId;
    }
}
