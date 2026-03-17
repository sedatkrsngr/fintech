namespace Fintech.IdentityService.Domain.ValueObjects;

public readonly record struct EmailVerificationTokenId(Guid Value)
{
    public static EmailVerificationTokenId New() => new(Guid.NewGuid());

    public static EmailVerificationTokenId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Email verification token id cannot be empty.", nameof(value));
        }

        return new EmailVerificationTokenId(value);
    }
}
