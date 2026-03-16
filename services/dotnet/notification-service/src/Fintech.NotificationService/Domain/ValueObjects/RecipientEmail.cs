namespace Fintech.NotificationService.Domain.ValueObjects;

public readonly record struct RecipientEmail(string Value)
{
    public static RecipientEmail Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Recipient email is required.", nameof(value));
        }

        var normalized = value.Trim().ToLowerInvariant();

        if (!normalized.Contains('@'))
        {
            throw new ArgumentException("Recipient email must be a valid email address.", nameof(value));
        }

        return new RecipientEmail(normalized);
    }

    public override string ToString() => Value;
}
