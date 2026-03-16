using Fintech.WalletService.Domain.ValueObjects;

namespace Fintech.WalletService.Application.Wallets.GetWalletById;

public sealed record GetWalletByIdQuery(WalletId WalletId);
