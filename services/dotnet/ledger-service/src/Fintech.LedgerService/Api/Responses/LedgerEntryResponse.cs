namespace Fintech.LedgerService.Api.Responses;

public sealed record LedgerEntryResponse(
    Guid LedgerEntryId,
    Guid WalletId,
    string ReferenceId,
    string EntryType,
    decimal Amount,
    string Currency,
    DateTime CreatedAtUtc);
