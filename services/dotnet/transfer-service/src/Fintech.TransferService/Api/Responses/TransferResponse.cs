namespace Fintech.TransferService.Api.Responses;

public sealed record TransferResponse(
    Guid TransferId,
    Guid SourceWalletId,
    Guid DestinationWalletId,
    string ReferenceId,
    decimal Amount,
    string Currency,
    string Status,
    DateTime CreatedAtUtc);
