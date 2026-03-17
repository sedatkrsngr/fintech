namespace Fintech.IdentityService.Domain.ValueObjects;

public readonly record struct RefreshTokenId(Guid Value)
{
    public static RefreshTokenId New() => new(Guid.NewGuid());

    public static RefreshTokenId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("Refresh token id cannot be empty.", nameof(value));
        }

        return new RefreshTokenId(value);
    }
}
