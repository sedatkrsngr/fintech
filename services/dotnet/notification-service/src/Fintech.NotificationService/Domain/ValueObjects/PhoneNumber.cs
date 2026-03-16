namespace Fintech.NotificationService.Domain.ValueObjects;

public readonly record struct PhoneNumber(string Value)
{
    public static PhoneNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Phone number is required.", nameof(value));
        }

        var normalized = value.Trim();

        if (normalized.Length < 7)
        {
            throw new ArgumentException("Phone number is too short.", nameof(value));
        }

        return new PhoneNumber(normalized);
    }

    public override string ToString() => Value;
}
