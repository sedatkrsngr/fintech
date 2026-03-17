namespace Fintech.IdentityService.Domain.ValueObjects;

public sealed record PermissionCode
{
    private PermissionCode(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static PermissionCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Permission code is required.", nameof(value));
        }

        var normalized = value.Trim().ToLowerInvariant();

        if (normalized.Length > 128)
        {
            throw new ArgumentException("Permission code cannot exceed 128 characters.", nameof(value));
        }

        return new PermissionCode(normalized);
    }

    public override string ToString() => Value;
}
