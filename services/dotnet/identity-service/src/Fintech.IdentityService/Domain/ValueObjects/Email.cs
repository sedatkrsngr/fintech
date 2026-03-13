namespace Fintech.IdentityService.Domain.ValueObjects;

public sealed record Email
{
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Email cannot be empty.", nameof(value));
        }

        var normalizedValue = value.Trim().ToLowerInvariant();

        if (!normalizedValue.Contains('@'))
        {
            throw new ArgumentException("Email format is invalid.", nameof(value));
        }

        return new Email(normalizedValue);
    }

    public override string ToString() => Value;
}
