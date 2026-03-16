namespace Fintech.TransferService.Api.Requests;

public sealed record CreateTransferRequest(
    Guid SourceWalletId,
    Guid DestinationWalletId,
    string ReferenceId,
    decimal Amount,
    string Currency);
