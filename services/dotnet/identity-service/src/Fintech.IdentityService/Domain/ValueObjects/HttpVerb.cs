namespace Fintech.IdentityService.Domain.ValueObjects;

public sealed record HttpVerb
{
    private static readonly HashSet<string> AllowedValues = new(StringComparer.OrdinalIgnoreCase)
    {
        "GET",
        "POST",
        "PUT",
        "PATCH",
        "DELETE",
        "HEAD",
        "OPTIONS"
    };

    private HttpVerb(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static HttpVerb Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Http verb is required.", nameof(value));
        }

        var normalized = value.Trim().ToUpperInvariant();

        if (!AllowedValues.Contains(normalized))
        {
            throw new ArgumentException("Http verb is invalid.", nameof(value));
        }

        return new HttpVerb(normalized);
    }

    public override string ToString() => Value;
}
