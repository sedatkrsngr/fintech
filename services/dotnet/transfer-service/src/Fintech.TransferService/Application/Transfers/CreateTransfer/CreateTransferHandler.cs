using Fintech.TransferService.Application.Abstractions;
using Fintech.TransferService.Domain.Entities;
using Fintech.TransferService.Domain.ValueObjects;

namespace Fintech.TransferService.Application.Transfers.CreateTransfer;

public sealed class CreateTransferHandler
{
    private readonly ITransferRepository _transferRepository;

    public CreateTransferHandler(ITransferRepository transferRepository)
    {
        _transferRepository = transferRepository;
    }

    public async Task<CreateTransferResult> HandleAsync(
        CreateTransferCommand command,
        CancellationToken cancellationToken = default)
    {
        var sourceWalletId = WalletId.From(command.SourceWalletId);
        var destinationWalletId = WalletId.From(command.DestinationWalletId);
        var referenceId = ReferenceId.Create(command.ReferenceId);
        var currency = Currency.Create(command.Currency);
        var amount = Money.Create(command.Amount, currency);

        var transfer = Transfer.Create(sourceWalletId, destinationWalletId, referenceId, amount);

        await _transferRepository.AddAsync(transfer, cancellationToken);

        return new CreateTransferResult(
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
