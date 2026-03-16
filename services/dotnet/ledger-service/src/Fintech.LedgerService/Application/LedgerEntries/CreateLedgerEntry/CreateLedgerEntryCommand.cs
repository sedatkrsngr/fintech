namespace Fintech.LedgerService.Application.LedgerEntries.CreateLedgerEntry;

public sealed record CreateLedgerEntryCommand(
    Guid WalletId,
    string ReferenceId,
    string EntryType,
    decimal Amount,
    string Currency);
