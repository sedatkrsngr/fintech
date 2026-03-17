namespace Fintech.IdentityService.Domain.ValueObjects;

public sealed record EndpointPattern
{
    private EndpointPattern(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static EndpointPattern Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Endpoint pattern is required.", nameof(value));
        }

        var normalized = value.Trim().ToLowerInvariant();

        if (!normalized.StartsWith('/'))
        {
            throw new ArgumentException("Endpoint pattern must start with '/'.", nameof(value));
        }

        if (normalized.Length > 256)
        {
            throw new ArgumentException("Endpoint pattern cannot exceed 256 characters.", nameof(value));
        }

        return new EndpointPattern(normalized);
    }

    public override string ToString() => Value;
}
