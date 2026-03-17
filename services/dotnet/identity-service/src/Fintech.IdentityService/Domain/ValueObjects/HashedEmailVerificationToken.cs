namespace Fintech.IdentityService.Domain.ValueObjects;

public sealed record HashedEmailVerificationToken
{
    private HashedEmailVerificationToken(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static HashedEmailVerificationToken Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Hashed email verification token is required.", nameof(value));
        }

        return new HashedEmailVerificationToken(value.Trim());
    }
}
