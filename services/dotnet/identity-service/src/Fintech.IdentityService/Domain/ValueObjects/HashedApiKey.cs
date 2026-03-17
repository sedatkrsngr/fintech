namespace Fintech.IdentityService.Domain.ValueObjects;

public sealed record HashedApiKey
{
    private HashedApiKey(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static HashedApiKey Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Hashed api key is required.", nameof(value));
        }

        var normalized = value.Trim();

        if (normalized.Length > 512)
        {
            throw new ArgumentException("Hashed api key cannot exceed 512 characters.", nameof(value));
        }

        return new HashedApiKey(normalized);
    }

    public override string ToString() => Value;
}
