using Fintech.WalletService.Domain.ValueObjects;

namespace Fintech.WalletService.Domain.Events;

public sealed record WalletCreated(
    WalletId WalletId,
    Guid OwnerUserId,
    Currency Currency,
    DateTime OccurredAtUtc);
