using Fintech.TransferService.Domain.Entities;
using Fintech.TransferService.Domain.ValueObjects;

namespace Fintech.TransferService.Application.Abstractions;

public interface ITransferRepository
{
    Task AddAsync(Transfer transfer, CancellationToken cancellationToken = default);

    Task<Transfer?> GetByIdAsync(TransferId transferId, CancellationToken cancellationToken = default);
}
