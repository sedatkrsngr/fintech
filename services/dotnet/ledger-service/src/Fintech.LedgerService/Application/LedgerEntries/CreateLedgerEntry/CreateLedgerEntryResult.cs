namespace Fintech.LedgerService.Application.LedgerEntries.CreateLedgerEntry;

public sealed record CreateLedgerEntryResult(
    Guid LedgerEntryId,
    Guid WalletId,
    string ReferenceId,
    string EntryType,
    decimal Amount,
    string Currency,
    DateTime CreatedAtUtc);
