namespace Fintech.WalletService.Api.Requests;

public sealed record CreateWalletRequest(Guid OwnerUserId, string Currency);
