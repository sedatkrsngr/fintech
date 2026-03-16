using Fintech.LedgerService.Domain.Entities;
using Fintech.LedgerService.Domain.ValueObjects;

namespace Fintech.LedgerService.Application.Abstractions;

public interface ILedgerEntryRepository
{
    Task AddAsync(LedgerEntry ledgerEntry, CancellationToken cancellationToken = default);

    Task<LedgerEntry?> GetByIdAsync(LedgerEntryId ledgerEntryId, CancellationToken cancellationToken = default);
}
