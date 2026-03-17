namespace Fintech.IdentityService.Domain.ValueObjects;

public sealed record RoleName
{
    private RoleName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static RoleName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Role name is required.", nameof(value));
        }

        var normalized = value.Trim();

        if (normalized.Length > 128)
        {
            throw new ArgumentException("Role name cannot exceed 128 characters.", nameof(value));
        }

        return new RoleName(normalized);
    }

    public override string ToString() => Value;
}
