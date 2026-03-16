namespace Fintech.LedgerService.Application.LedgerEntries.GetLedgerEntryById;

public sealed record GetLedgerEntryByIdResult(
    Guid LedgerEntryId,
    Guid WalletId,
    string ReferenceId,
    string EntryType,
    decimal Amount,
    string Currency,
    DateTime CreatedAtUtc);
