using Fintech.WalletService.Domain.Entities;
using Fintech.WalletService.Domain.ValueObjects;

namespace Fintech.WalletService.Application.Abstractions;

public interface IWalletRepository
{
    Task AddAsync(Wallet wallet, CancellationToken cancellationToken = default);

    Task<Wallet?> GetByIdAsync(WalletId walletId, CancellationToken cancellationToken = default);
}
