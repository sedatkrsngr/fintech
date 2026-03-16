using Fintech.LedgerService.Domain.ValueObjects;

namespace Fintech.LedgerService.Application.LedgerEntries.GetLedgerEntryById;

public sealed record GetLedgerEntryByIdQuery(LedgerEntryId LedgerEntryId);
