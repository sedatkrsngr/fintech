using Fintech.WalletService.Domain.Enums;
using Fintech.WalletService.Domain.ValueObjects;

namespace Fintech.WalletService.Domain.Entities;

public sealed class Wallet
{
    private Wallet()
    {
        Balance = null!;
    }

    private Wallet(WalletId id, Guid ownerUserId, Money balance)
    {
        Id = id;
        OwnerUserId = ownerUserId;
        Balance = balance;
        Status = WalletStatus.Active;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public WalletId Id { get; private set; }

    public Guid OwnerUserId { get; private set; }

    public Money Balance { get; private set; }

    public WalletStatus Status { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public static Wallet Create(Guid ownerUserId, Currency currency)
    {
        if (ownerUserId == Guid.Empty)
        {
            throw new ArgumentException("Owner user id cannot be empty.", nameof(ownerUserId));
        }

        return new Wallet(
            WalletId.New(),
            ownerUserId,
            Money.Create(0m, currency));
    }

    public void Credit(Money amount)
    {
        EnsureActive();
        Balance = Balance.Add(amount);
    }

    public void Debit(Money amount)
    {
        EnsureActive();
        Balance = Balance.Subtract(amount);
    }

    public void Suspend()
    {
        Status = WalletStatus.Suspended;
    }

    private void EnsureActive()
    {
        if (Status != WalletStatus.Active)
        {
            throw new InvalidOperationException("Wallet must be active for balance operations.");
        }
    }
}
