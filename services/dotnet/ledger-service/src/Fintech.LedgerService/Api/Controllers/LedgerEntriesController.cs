using Fintech.LedgerService.Api.Requests;
using Fintech.LedgerService.Api.Responses;
using Fintech.LedgerService.Application.LedgerEntries.CreateLedgerEntry;
using Fintech.LedgerService.Application.LedgerEntries.GetLedgerEntryById;
using Fintech.LedgerService.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.LedgerService.Api.Controllers;

[ApiController]
[Route("api/ledger-entries")]
public sealed class LedgerEntriesController : ControllerBase
{
    [HttpGet("{ledgerEntryId:guid}")]
    public async Task<ActionResult<LedgerEntryResponse>> GetById(
        Guid ledgerEntryId,
        [FromServices] GetLedgerEntryByIdHandler handler)
    {
        var query = new GetLedgerEntryByIdQuery(LedgerEntryId.From(ledgerEntryId));
        var result = await handler.HandleAsync(query, HttpContext.RequestAborted);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(new LedgerEntryResponse(
            result.LedgerEntryId,
            result.WalletId,
            result.ReferenceId,
            result.EntryType,
            result.Amount,
            result.Currency,
            result.CreatedAtUtc));
    }

    [HttpPost]
    public async Task<ActionResult<LedgerEntryResponse>> Create(
        [FromBody] CreateLedgerEntryRequest request,
        [FromServices] CreateLedgerEntryHandler handler)
    {
        var command = new CreateLedgerEntryCommand(
            request.WalletId,
            request.ReferenceId,
            request.EntryType,
            request.Amount,
            request.Currency);

        var result = await handler.HandleAsync(command, HttpContext.RequestAborted);

        var response = new LedgerEntryResponse(
            result.LedgerEntryId,
            result.WalletId,
            result.ReferenceId,
            result.EntryType,
            result.Amount,
            result.Currency,
            result.CreatedAtUtc);

        return CreatedAtAction(nameof(GetById), new { ledgerEntryId = response.LedgerEntryId }, response);
    }
}
