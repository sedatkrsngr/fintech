using Fintech.LedgerService.Application.Abstractions;
using Fintech.LedgerService.Domain.Entities;
using Fintech.LedgerService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Fintech.LedgerService.Infrastructure.Persistence.Repositories.EfCore;

public sealed class LedgerEntryRepository : ILedgerEntryRepository
{
    private readonly LedgerDbContext _dbContext;

    public LedgerEntryRepository(LedgerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(LedgerEntry ledgerEntry, CancellationToken cancellationToken = default)
    {
        await _dbContext.LedgerEntries.AddAsync(ledgerEntry, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<LedgerEntry?> GetByIdAsync(LedgerEntryId ledgerEntryId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.LedgerEntries
            .FirstOrDefaultAsync(x => x.Id == ledgerEntryId, cancellationToken);
    }
}
