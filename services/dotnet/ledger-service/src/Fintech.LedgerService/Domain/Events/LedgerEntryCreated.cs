using Fintech.LedgerService.Domain.ValueObjects;

namespace Fintech.LedgerService.Domain.Events;

public sealed record LedgerEntryCreated(
    LedgerEntryId LedgerEntryId,
    WalletId WalletId,
    ReferenceId ReferenceId,
    DateTime OccurredAtUtc);
