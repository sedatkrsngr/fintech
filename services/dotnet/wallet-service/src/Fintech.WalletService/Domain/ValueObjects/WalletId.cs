namespace Fintech.WalletService.Domain.ValueObjects;

public readonly record struct WalletId(Guid Value)
{
    public static WalletId New() => new(Guid.NewGuid());

    public static WalletId From(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("WalletId cannot be empty.", nameof(value));
        }

        return new WalletId(value);
    }

    public override string ToString() => Value.ToString();
}
