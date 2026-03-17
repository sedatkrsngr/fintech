namespace Fintech.IdentityService.Domain.ValueObjects;

public sealed record ApiClientName
{
    private ApiClientName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static ApiClientName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Api client name is required.", nameof(value));
        }

        var normalized = value.Trim();

        if (normalized.Length > 128)
        {
            throw new ArgumentException("Api client name cannot exceed 128 characters.", nameof(value));
        }

        return new ApiClientName(normalized);
    }

    public override string ToString() => Value;
}
