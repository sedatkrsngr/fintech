using Fintech.IdentityService.Api.Requests;
using Fintech.IdentityService.Api.Responses;
using Fintech.IdentityService.Application.ApiAccess.CreateApiClient;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.IdentityService.Api.Controllers;

[ApiController]
[Route("api/api-clients")]
public sealed class ApiClientsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CreateApiClientResponse>> Create(
        [FromBody] CreateApiClientRequest request,
        [FromServices] CreateApiClientHandler handler)
    {
        var result = await handler.HandleAsync(
            new CreateApiClientCommand(request.Name, request.AllowedIps),
            HttpContext.RequestAborted);

        return Ok(new CreateApiClientResponse(
            result.ApiClientId,
            result.Name,
            result.ApiKey,
            result.CreatedAtUtc,
            result.AllowedIps));
    }
}
