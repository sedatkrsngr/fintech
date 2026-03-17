using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Domain.Entities;

public sealed class EmailVerificationToken
{
    private EmailVerificationToken(
        EmailVerificationTokenId id,
        UserId userId,
        HashedEmailVerificationToken hashedToken,
        DateTime expiresAtUtc)
    {
        if (expiresAtUtc <= DateTime.UtcNow)
        {
            throw new ArgumentException("Email verification token must expire in the future.", nameof(expiresAtUtc));
        }

        Id = id;
        UserId = userId;
        HashedToken = hashedToken;
        ExpiresAtUtc = expiresAtUtc;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public EmailVerificationTokenId Id { get; }

    public UserId UserId { get; }

    public HashedEmailVerificationToken HashedToken { get; private set; }

    public DateTime ExpiresAtUtc { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public DateTime? ConsumedAtUtc { get; private set; }

    public static EmailVerificationToken Issue(
        UserId userId,
        HashedEmailVerificationToken hashedToken,
        DateTime expiresAtUtc)
    {
        return new EmailVerificationToken(EmailVerificationTokenId.New(), userId, hashedToken, expiresAtUtc);
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
