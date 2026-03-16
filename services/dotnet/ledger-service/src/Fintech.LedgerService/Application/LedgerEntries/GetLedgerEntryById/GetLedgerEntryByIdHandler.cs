using Fintech.LedgerService.Application.Abstractions;

namespace Fintech.LedgerService.Application.LedgerEntries.GetLedgerEntryById;

public sealed class GetLedgerEntryByIdHandler
{
    private readonly ILedgerEntryRepository _ledgerEntryRepository;

    public GetLedgerEntryByIdHandler(ILedgerEntryRepository ledgerEntryRepository)
    {
        _ledgerEntryRepository = ledgerEntryRepository;
    }

    public async Task<GetLedgerEntryByIdResult?> HandleAsync(
        GetLedgerEntryByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var ledgerEntry = await _ledgerEntryRepository.GetByIdAsync(query.LedgerEntryId, cancellationToken);

        if (ledgerEntry is null)
        {
            return null;
        }

        return new GetLedgerEntryByIdResult(
            ledgerEntry.Id.Value,
            ledgerEntry.WalletId.Value,
            ledgerEntry.ReferenceId.Value,
            ledgerEntry.EntryType.ToString(),
            ledgerEntry.Amount.Amount,
            ledgerEntry.Amount.Currency.Code,
            ledgerEntry.CreatedAtUtc);
    }
}
