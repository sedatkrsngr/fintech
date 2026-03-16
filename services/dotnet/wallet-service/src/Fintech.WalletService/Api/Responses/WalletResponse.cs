namespace Fintech.WalletService.Api.Responses;

public sealed record WalletResponse(
    Guid WalletId,
    Guid OwnerUserId,
    decimal Balance,
    string Currency,
    string Status,
    DateTime CreatedAtUtc);
