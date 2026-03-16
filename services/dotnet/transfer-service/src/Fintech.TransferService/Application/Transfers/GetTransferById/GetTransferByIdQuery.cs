using Fintech.TransferService.Domain.ValueObjects;

namespace Fintech.TransferService.Application.Transfers.GetTransferById;

public sealed record GetTransferByIdQuery(TransferId TransferId);
