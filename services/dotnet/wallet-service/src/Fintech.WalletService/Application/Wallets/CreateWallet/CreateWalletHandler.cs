using Fintech.WalletService.Application.Abstractions;
using Fintech.WalletService.Domain.Entities;
using Fintech.WalletService.Domain.ValueObjects;

namespace Fintech.WalletService.Application.Wallets.CreateWallet;

public sealed class CreateWalletHandler
{
    private readonly IWalletRepository _walletRepository;

    public CreateWalletHandler(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<CreateWalletResult> HandleAsync(
        CreateWalletCommand command,
        CancellationToken cancellationToken = default)
    {
        var currency = Currency.Create(command.Currency);
        var wallet = Wallet.Create(command.OwnerUserId, currency);

        await _walletRepository.AddAsync(wallet, cancellationToken);

        return new CreateWalletResult(
            wallet.Id.Value,
            wallet.OwnerUserId,
            wallet.Balance.Amount,
            wallet.Balance.Currency.Code,
            wallet.Status.ToString(),
            wallet.CreatedAtUtc);
    }
}
