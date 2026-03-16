namespace Fintech.TransferService.Domain.ValueObjects;

public sealed record Money
{
    private Money()
    {
        Currency = null!;
    }

    private Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public decimal Amount { get; private set; }

    public Currency Currency { get; private set; }

    public static Money Create(decimal amount, Currency currency)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Money amount must be greater than zero.", nameof(amount));
        }

        return new Money(decimal.Round(amount, 2, MidpointRounding.ToEven), currency);
    }
}
