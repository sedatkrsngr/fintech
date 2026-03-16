using Fintech.TransferService.Domain.ValueObjects;

namespace Fintech.TransferService.Domain.Events;

public sealed record TransferCreated(
    TransferId TransferId,
    WalletId SourceWalletId,
    WalletId DestinationWalletId,
    ReferenceId ReferenceId,
    DateTime OccurredAtUtc);
