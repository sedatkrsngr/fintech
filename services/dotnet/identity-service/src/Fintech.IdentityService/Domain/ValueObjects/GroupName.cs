namespace Fintech.IdentityService.Domain.ValueObjects;

public sealed record GroupName
{
    private GroupName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static GroupName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Group name is required.", nameof(value));
        }

        var normalized = value.Trim();

        if (normalized.Length > 128)
        {
            throw new ArgumentException("Group name cannot exceed 128 characters.", nameof(value));
        }

        return new GroupName(normalized);
    }

    public override string ToString() => Value;
}
