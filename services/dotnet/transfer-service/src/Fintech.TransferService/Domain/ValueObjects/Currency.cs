namespace Fintech.TransferService.Domain.ValueObjects;

public sealed record Currency
{
    private Currency()
    {
        Code = null!;
    }

    private Currency(string code)
    {
        Code = code;
    }

    public string Code { get; private set; }

    public static Currency Create(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ArgumentException("Currency code cannot be empty.", nameof(code));
        }

        var normalizedCode = code.Trim().ToUpperInvariant();

        if (normalizedCode.Length != 3)
        {
            throw new ArgumentException("Currency code must be 3 characters.", nameof(code));
        }

        return new Currency(normalizedCode);
    }
}
