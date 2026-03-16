using Fintech.WalletService.Application.Abstractions;

namespace Fintech.WalletService.Application.Wallets.GetWalletById;

public sealed class GetWalletByIdHandler
{
    private readonly IWalletRepository _walletRepository;

    public GetWalletByIdHandler(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }

    public async Task<GetWalletByIdResult?> HandleAsync(
        GetWalletByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var wallet = await _walletRepository.GetByIdAsync(query.WalletId, cancellationToken);

        if (wallet is null)
        {
            return null;
        }

        return new GetWalletByIdResult(
            wallet.Id.Value,
            wallet.OwnerUserId,
            wallet.Balance.Amount,
            wallet.Balance.Currency.Code,
            wallet.Status.ToString(),
            wallet.CreatedAtUtc);
    }
}
