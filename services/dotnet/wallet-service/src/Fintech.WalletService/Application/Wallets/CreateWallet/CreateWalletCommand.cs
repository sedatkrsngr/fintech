namespace Fintech.WalletService.Application.Wallets.CreateWallet;

public sealed record CreateWalletCommand(Guid OwnerUserId, string Currency);
