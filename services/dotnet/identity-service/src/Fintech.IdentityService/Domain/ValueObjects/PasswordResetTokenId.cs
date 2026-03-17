namespace Fintech.IdentityService.Domain.ValueObjects;

public readonly record struct PasswordResetTokenId(Guid Value)
{
    public static PasswordResetTokenId New() => new(Guid.NewGuid());

    public static PasswordResetTokenId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Password reset token id cannot be empty.", nameof(value));
        }

        return new PasswordResetTokenId(value);
    }
}
