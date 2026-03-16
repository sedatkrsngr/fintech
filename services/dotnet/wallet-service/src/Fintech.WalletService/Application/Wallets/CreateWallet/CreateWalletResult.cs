namespace Fintech.WalletService.Application.Wallets.CreateWallet;

public sealed record CreateWalletResult(
    Guid WalletId,
    Guid OwnerUserId,
    decimal Balance,
    string Currency,
    string Status,
    DateTime CreatedAtUtc);
