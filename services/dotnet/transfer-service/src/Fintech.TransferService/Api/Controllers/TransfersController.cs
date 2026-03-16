using Fintech.TransferService.Api.Requests;
using Fintech.TransferService.Api.Responses;
using Fintech.TransferService.Application.Transfers.CreateTransfer;
using Fintech.TransferService.Application.Transfers.GetTransferById;
using Fintech.TransferService.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.TransferService.Api.Controllers;

[ApiController]
[Route("api/transfers")]
public sealed class TransfersController : ControllerBase
{
    [HttpGet("{transferId:guid}")]
    public async Task<ActionResult<TransferResponse>> GetById(
        Guid transferId,
        [FromServices] GetTransferByIdHandler handler)
    {
        var query = new GetTransferByIdQuery(TransferId.From(transferId));
        var result = await handler.HandleAsync(query, HttpContext.RequestAborted);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(new TransferResponse(
            result.TransferId,
            result.SourceWalletId,
            result.DestinationWalletId,
            result.ReferenceId,
            result.Amount,
            result.Currency,
            result.Status,
            result.CreatedAtUtc));
    }

    [HttpPost]
    public async Task<ActionResult<TransferResponse>> Create(
        [FromBody] CreateTransferRequest request,
        [FromServices] CreateTransferHandler handler)
    {
        var command = new CreateTransferCommand(
            request.SourceWalletId,
            request.DestinationWalletId,
            request.ReferenceId,
            request.Amount,
            request.Currency);

        var result = await handler.HandleAsync(command, HttpContext.RequestAborted);

        var response = new TransferResponse(
            result.TransferId,
            result.SourceWalletId,
            result.DestinationWalletId,
            result.ReferenceId,
            result.Amount,
            result.Currency,
            result.Status,
            result.CreatedAtUtc);

        return CreatedAtAction(nameof(GetById), new { transferId = response.TransferId }, response);
    }
}
