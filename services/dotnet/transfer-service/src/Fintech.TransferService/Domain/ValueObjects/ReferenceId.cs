namespace Fintech.TransferService.Domain.ValueObjects;

public sealed record ReferenceId
{
    private ReferenceId()
    {
        Value = null!;
    }

    private ReferenceId(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static ReferenceId Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("ReferenceId cannot be empty.", nameof(value));
        }

        return new ReferenceId(value.Trim());
    }
}
