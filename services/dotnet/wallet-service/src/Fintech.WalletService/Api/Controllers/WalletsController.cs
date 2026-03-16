using Fintech.WalletService.Api.Requests;
using Fintech.WalletService.Api.Responses;
using Fintech.WalletService.Application.Wallets.CreateWallet;
using Fintech.WalletService.Application.Wallets.GetWalletById;
using Fintech.WalletService.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.WalletService.Api.Controllers;

[ApiController]
[Route("api/wallets")]
public sealed class WalletsController : ControllerBase
{
    [HttpGet("{walletId:guid}")]
    public async Task<ActionResult<WalletResponse>> GetById(
        Guid walletId,
        [FromServices] GetWalletByIdHandler handler)
    {
        var query = new GetWalletByIdQuery(WalletId.From(walletId));
        var result = await handler.HandleAsync(query, HttpContext.RequestAborted);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(new WalletResponse(
            result.WalletId,
            result.OwnerUserId,
            result.Balance,
            result.Currency,
            result.Status,
            result.CreatedAtUtc));
    }

    [HttpPost]
    public async Task<ActionResult<WalletResponse>> Create(
        [FromBody] CreateWalletRequest request,
        [FromServices] CreateWalletHandler handler)
    {
        var command = new CreateWalletCommand(request.OwnerUserId, request.Currency);
        var result = await handler.HandleAsync(command, HttpContext.RequestAborted);

        var response = new WalletResponse(
            result.WalletId,
            result.OwnerUserId,
            result.Balance,
            result.Currency,
            result.Status,
            result.CreatedAtUtc);

        return CreatedAtAction(nameof(GetById), new { walletId = response.WalletId }, response);
    }
}
