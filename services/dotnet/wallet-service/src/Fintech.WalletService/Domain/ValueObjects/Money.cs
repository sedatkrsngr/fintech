namespace Fintech.WalletService.Domain.ValueObjects;

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
        if (amount < 0)
        {
            throw new ArgumentException("Money amount cannot be negative.", nameof(amount));
        }

        return new Money(decimal.Round(amount, 2, MidpointRounding.ToEven), currency);
    }

    public Money Add(Money other)
    {
        EnsureSameCurrency(other);

        return Create(Amount + other.Amount, Currency);
    }

    public Money Subtract(Money other)
    {
        EnsureSameCurrency(other);

        var newAmount = Amount - other.Amount;

        if (newAmount < 0)
        {
            throw new InvalidOperationException("Money amount cannot be negative after subtraction.");
        }

        return Create(newAmount, Currency);
    }

    private void EnsureSameCurrency(Money other)
    {
        if (Currency != other.Currency)
        {
            throw new InvalidOperationException("Money operations require matching currencies.");
        }
    }

    public override string ToString() => $"{Amount:0.00} {Currency}";
}
