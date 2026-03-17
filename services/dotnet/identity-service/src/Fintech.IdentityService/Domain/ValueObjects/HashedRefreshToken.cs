namespace Fintech.IdentityService.Domain.ValueObjects;

public sealed record HashedRefreshToken
{
    private HashedRefreshToken(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static HashedRefreshToken Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Hashed refresh token is required.", nameof(value));
        }

        return new HashedRefreshToken(value.Trim());
    }
}
