using Fintech.IdentityService.Api.Requests;
using Fintech.IdentityService.Api.Responses;
using Fintech.IdentityService.Application.Auth.IssueToken;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.IdentityService.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    [HttpPost("token")]
    public async Task<ActionResult<IssueTokenResponse>> IssueToken(
        [FromBody] IssueTokenRequest request,
        [FromServices] IssueTokenHandler handler)
    {
        var result = await handler.HandleAsync(
            new IssueTokenCommand(request.Email, request.Password),
            HttpContext.RequestAborted);

        if (result is null)
        {
            return Unauthorized();
        }

        return Ok(new IssueTokenResponse(
            result.AccessToken,
            result.ExpiresAtUtc,
            result.UserId,
            result.Email));
    }
}
