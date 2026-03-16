namespace Fintech.TransferService.Application.Transfers.GetTransferById;

public sealed record GetTransferByIdResult(
    Guid TransferId,
    Guid SourceWalletId,
    Guid DestinationWalletId,
    string ReferenceId,
    decimal Amount,
    string Currency,
    string Status,
    DateTime CreatedAtUtc);
