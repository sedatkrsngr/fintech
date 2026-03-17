using Fintech.IdentityService.Api.Requests;
using Fintech.IdentityService.Api.Responses;
using Fintech.IdentityService.Application.AccessRules.EvaluateAccess;
using Fintech.IdentityService.Application.ApiAccess.ValidateApiKey;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.IdentityService.Api.Controllers;

[ApiController]
[Route("api/internal/auth")]
public sealed class InternalAuthController : ControllerBase
{
    [HttpPost("validate-api-key")]
    public async Task<ActionResult<ValidateApiKeyResponse>> ValidateApiKey(
        [FromBody] ValidateApiKeyRequest request,
        [FromServices] ValidateApiKeyHandler handler)
    {
        var result = await handler.HandleAsync(
            new ValidateApiKeyCommand(request.ApiKey, request.RemoteIp),
            HttpContext.RequestAborted);

        if (result is null)
        {
            return Unauthorized();
        }

        return Ok(new ValidateApiKeyResponse(result.ApiClientId, result.Name));
    }

    [HttpPost("evaluate-access")]
    public async Task<ActionResult<EvaluateAccessResponse>> EvaluateAccess(
        [FromBody] EvaluateAccessRequest request,
        [FromServices] EvaluateAccessHandler handler)
    {
        var result = await handler.HandleAsync(
            new EvaluateAccessCommand(
                request.SubjectType,
                request.SubjectId,
                request.Path,
                request.HttpMethod),
            HttpContext.RequestAborted);

        return Ok(new EvaluateAccessResponse(result.IsAllowed));
    }
}
