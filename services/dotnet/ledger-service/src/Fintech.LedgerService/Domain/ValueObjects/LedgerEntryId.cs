namespace Fintech.LedgerService.Domain.ValueObjects;

public readonly record struct LedgerEntryId(Guid Value)
{
    public static LedgerEntryId New() => new(Guid.NewGuid());

    public static LedgerEntryId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("LedgerEntryId cannot be empty.", nameof(value));
        }

        return new LedgerEntryId(value);
    }

    public override string ToString() => Value.ToString();
}
