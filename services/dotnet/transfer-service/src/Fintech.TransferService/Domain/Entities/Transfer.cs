using Fintech.TransferService.Domain.Enums;
using Fintech.TransferService.Domain.ValueObjects;

namespace Fintech.TransferService.Domain.Entities;

public sealed class Transfer
{
    private Transfer()
    {
        SourceWalletId = default;
        DestinationWalletId = default;
        ReferenceId = null!;
        Amount = null!;
    }

    private Transfer(
        TransferId id,
        WalletId sourceWalletId,
        WalletId destinationWalletId,
        ReferenceId referenceId,
        Money amount)
    {
        if (sourceWalletId == destinationWalletId)
        {
            throw new InvalidOperationException("Source and destination wallets must be different.");
        }

        Id = id;
        SourceWalletId = sourceWalletId;
        DestinationWalletId = destinationWalletId;
        ReferenceId = referenceId;
        Amount = amount;
        Status = TransferStatus.Pending;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public TransferId Id { get; private set; }

    public WalletId SourceWalletId { get; private set; }

    public WalletId DestinationWalletId { get; private set; }

    public ReferenceId ReferenceId { get; private set; }

    public Money Amount { get; private set; }

    public TransferStatus Status { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public static Transfer Create(
        WalletId sourceWalletId,
        WalletId destinationWalletId,
        ReferenceId referenceId,
        Money amount)
    {
        return new Transfer(
            TransferId.New(),
            sourceWalletId,
            destinationWalletId,
            referenceId,
            amount);
    }

    public void MarkCompleted()
    {
        Status = TransferStatus.Completed;
    }

    public void MarkFailed()
    {
        Status = TransferStatus.Failed;
    }
}
