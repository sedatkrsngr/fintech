namespace Fintech.TransferService.Domain.ValueObjects;

public readonly record struct TransferId(Guid Value)
{
    public static TransferId New() => new(Guid.NewGuid());

    public static TransferId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("TransferId cannot be empty.", nameof(value));
        }

        return new TransferId(value);
    }
}
