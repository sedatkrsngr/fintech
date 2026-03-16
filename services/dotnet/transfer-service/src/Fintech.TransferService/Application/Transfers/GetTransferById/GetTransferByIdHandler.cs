using Fintech.TransferService.Application.Abstractions;

namespace Fintech.TransferService.Application.Transfers.GetTransferById;

public sealed class GetTransferByIdHandler
{
    private readonly ITransferRepository _transferRepository;

    public GetTransferByIdHandler(ITransferRepository transferRepository)
    {
        _transferRepository = transferRepository;
    }

    public async Task<GetTransferByIdResult?> HandleAsync(
        GetTransferByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var transfer = await _transferRepository.GetByIdAsync(query.TransferId, cancellationToken);

        if (transfer is null)
        {
            return null;
        }

        return new GetTransferByIdResult(
            transfer.Id.Value,
            transfer.SourceWalletId.Value,
            transfer.DestinationWalletId.Value,
            transfer.ReferenceId.Value,
            transfer.Amount.Amount,
            transfer.Amount.Currency.Code,
            transfer.Status.ToString(),
            transfer.CreatedAtUtc);
    }
}
