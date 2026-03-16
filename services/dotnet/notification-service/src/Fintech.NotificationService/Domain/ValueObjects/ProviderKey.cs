namespace Fintech.NotificationService.Domain.ValueObjects;

public readonly record struct ProviderKey(string Value)
{
    public static ProviderKey Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Provider key is required.", nameof(value));
        }

        var normalized = value.Trim().ToLowerInvariant();

        if (normalized.Length > 128)
        {
            throw new ArgumentException("Provider key cannot exceed 128 characters.", nameof(value));
        }

        return new ProviderKey(normalized);
    }

    public override string ToString() => Value;
}
