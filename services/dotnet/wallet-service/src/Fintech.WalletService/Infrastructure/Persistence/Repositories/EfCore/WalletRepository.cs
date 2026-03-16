using Fintech.WalletService.Application.Abstractions;
using Fintech.WalletService.Domain.Entities;
using Fintech.WalletService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Fintech.WalletService.Infrastructure.Persistence.Repositories.EfCore;

public sealed class WalletRepository : IWalletRepository
{
    private readonly WalletDbContext _dbContext;

    public WalletRepository(WalletDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Wallet wallet, CancellationToken cancellationToken = default)
    {
        await _dbContext.Wallets.AddAsync(wallet, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Wallet?> GetByIdAsync(WalletId walletId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Wallets
            .FirstOrDefaultAsync(x => x.Id == walletId, cancellationToken);
    }
}
