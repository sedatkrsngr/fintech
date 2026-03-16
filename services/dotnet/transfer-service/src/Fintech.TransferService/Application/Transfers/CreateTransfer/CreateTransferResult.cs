namespace Fintech.TransferService.Application.Transfers.CreateTransfer;

public sealed record CreateTransferResult(
    Guid TransferId,
    Guid SourceWalletId,
    Guid DestinationWalletId,
    string ReferenceId,
    decimal Amount,
    string Currency,
    string Status,
    DateTime CreatedAtUtc);
