using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Domain.Entities;

public sealed class PasswordResetToken
{
    private PasswordResetToken(
        PasswordResetTokenId id,
        UserId userId,
        HashedPasswordResetToken hashedToken,
        DateTime expiresAtUtc)
    {
        if (expiresAtUtc <= DateTime.UtcNow)
        {
            throw new ArgumentException("Password reset token must expire in the future.", nameof(expiresAtUtc));
        }

        Id = id;
        UserId = userId;
        HashedToken = hashedToken;
        ExpiresAtUtc = expiresAtUtc;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public PasswordResetTokenId Id { get; }

    public UserId UserId { get; }

    public HashedPasswordResetToken HashedToken { get; private set; }

    public DateTime ExpiresAtUtc { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public DateTime? ConsumedAtUtc { get; private set; }

    public static PasswordResetToken Issue(
        UserId userId,
        HashedPasswordResetToken hashedToken,
        DateTime expiresAtUtc)
    {
        return new PasswordResetToken(PasswordResetTokenId.New(), userId, hashedToken, expiresAtUtc);
    }

    public bool IsActiveAt(DateTime utcNow)
    {
        return ConsumedAtUtc is null && ExpiresAtUtc > utcNow;
    }

    public void Consume()
    {
        ConsumedAtUtc ??= DateTime.UtcNow;
    }
}
