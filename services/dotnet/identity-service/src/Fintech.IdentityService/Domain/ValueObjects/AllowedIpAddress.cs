using System.Net;

namespace Fintech.IdentityService.Domain.ValueObjects;

public sealed record AllowedIpAddress
{
    private AllowedIpAddress(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static AllowedIpAddress Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Allowed IP address is required.", nameof(value));
        }

        var normalized = value.Trim();

        if (!IPAddress.TryParse(normalized, out _))
        {
            throw new ArgumentException("Allowed IP address is invalid.", nameof(value));
        }

        return new AllowedIpAddress(normalized);
    }

    public override string ToString() => Value;
}
