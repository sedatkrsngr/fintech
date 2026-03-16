using Fintech.TransferService.Application.Abstractions;
using Fintech.TransferService.Domain.Entities;
using Fintech.TransferService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Fintech.TransferService.Infrastructure.Persistence.Repositories.EfCore;

public sealed class TransferRepository : ITransferRepository
{
    private readonly TransferDbContext _dbContext;

    public TransferRepository(TransferDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Transfer transfer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Transfers.AddAsync(transfer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Transfer?> GetByIdAsync(TransferId transferId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Transfers
            .FirstOrDefaultAsync(x => x.Id == transferId, cancellationToken);
    }
}
