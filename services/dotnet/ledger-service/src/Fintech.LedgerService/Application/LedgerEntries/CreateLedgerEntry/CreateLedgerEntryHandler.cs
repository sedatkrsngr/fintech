using Fintech.LedgerService.Application.Abstractions;
using Fintech.LedgerService.Domain.Entities;
using Fintech.LedgerService.Domain.Enums;
using Fintech.LedgerService.Domain.ValueObjects;

namespace Fintech.LedgerService.Application.LedgerEntries.CreateLedgerEntry;

public sealed class CreateLedgerEntryHandler
{
    private readonly ILedgerEntryRepository _ledgerEntryRepository;

    public CreateLedgerEntryHandler(ILedgerEntryRepository ledgerEntryRepository)
    {
        _ledgerEntryRepository = ledgerEntryRepository;
    }

    public async Task<CreateLedgerEntryResult> HandleAsync(
        CreateLedgerEntryCommand command,
        CancellationToken cancellationToken = default)
    {
        var walletId = WalletId.From(command.WalletId);
        var referenceId = ReferenceId.Create(command.ReferenceId);
        var currency = Currency.Create(command.Currency);
        var amount = Money.Create(command.Amount, currency);
        if (!Enum.TryParse<LedgerEntryType>(command.EntryType, true, out var entryType))
        {
            throw new ArgumentException("Ledger entry type is invalid.", nameof(command.EntryType));
        }

        var ledgerEntry = LedgerEntry.Create(walletId, referenceId, entryType, amount);

        await _ledgerEntryRepository.AddAsync(ledgerEntry, cancellationToken);

        return new CreateLedgerEntryResult(
            ledgerEntry.Id.Value,
            ledgerEntry.WalletId.Value,
            ledgerEntry.ReferenceId.Value,
            ledgerEntry.EntryType.ToString(),
            ledgerEntry.Amount.Amount,
            ledgerEntry.Amount.Currency.Code,
            ledgerEntry.CreatedAtUtc);
    }
}
