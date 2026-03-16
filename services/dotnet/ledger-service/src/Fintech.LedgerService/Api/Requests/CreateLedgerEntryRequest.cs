namespace Fintech.LedgerService.Api.Requests;

public sealed record CreateLedgerEntryRequest(
    Guid WalletId,
    string ReferenceId,
    string EntryType,
    decimal Amount,
    string Currency);
