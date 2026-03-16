using Fintech.LedgerService.Domain.Enums;
using Fintech.LedgerService.Domain.ValueObjects;

namespace Fintech.LedgerService.Domain.Entities;

public sealed class LedgerEntry
{
    private LedgerEntry()
    {
        WalletId = default;
        ReferenceId = null!;
        Amount = null!;
    }

    private LedgerEntry(
        LedgerEntryId id,
        WalletId walletId,
        ReferenceId referenceId,
        LedgerEntryType entryType,
        Money amount)
    {
        Id = id;
        WalletId = walletId;
        ReferenceId = referenceId;
        EntryType = entryType;
        Amount = amount;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public LedgerEntryId Id { get; private set; }

    public WalletId WalletId { get; private set; }

    public ReferenceId ReferenceId { get; private set; }

    public LedgerEntryType EntryType { get; private set; }

    public Money Amount { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public static LedgerEntry Create(
        WalletId walletId,
        ReferenceId referenceId,
        LedgerEntryType entryType,
        Money amount)
    {
        return new LedgerEntry(
            LedgerEntryId.New(),
            walletId,
            referenceId,
            entryType,
            amount);
    }
}
