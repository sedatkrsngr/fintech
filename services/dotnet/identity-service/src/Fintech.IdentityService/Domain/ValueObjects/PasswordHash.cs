namespace Fintech.IdentityService.Domain.ValueObjects;

public sealed record PasswordHash
{
    private PasswordHash(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static PasswordHash Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Password hash is required.", nameof(value));
        }

        var normalized = value.Trim();

        if (normalized.Length > 512)
        {
            throw new ArgumentException("Password hash cannot exceed 512 characters.", nameof(value));
        }

        return new PasswordHash(normalized);
    }

    public override string ToString() => Value;
}
