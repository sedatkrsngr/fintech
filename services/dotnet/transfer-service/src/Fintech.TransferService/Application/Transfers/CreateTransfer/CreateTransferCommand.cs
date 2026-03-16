namespace Fintech.TransferService.Application.Transfers.CreateTransfer;

public sealed record CreateTransferCommand(
    Guid SourceWalletId,
    Guid DestinationWalletId,
    string ReferenceId,
    decimal Amount,
    string Currency);
