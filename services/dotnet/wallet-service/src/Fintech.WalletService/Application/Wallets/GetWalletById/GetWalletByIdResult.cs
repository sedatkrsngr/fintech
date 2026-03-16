namespace Fintech.WalletService.Application.Wallets.GetWalletById;

public sealed record GetWalletByIdResult(
    Guid WalletId,
    Guid OwnerUserId,
    decimal Balance,
    string Currency,
    string Status,
    DateTime CreatedAtUtc);
