namespace Fintech.IdentityService.Domain.ValueObjects;

public sealed record HashedPasswordResetToken
{
    private HashedPasswordResetToken(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static HashedPasswordResetToken Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Hashed password reset token is required.", nameof(value));
        }

        return new HashedPasswordResetToken(value.Trim());
    }
}
