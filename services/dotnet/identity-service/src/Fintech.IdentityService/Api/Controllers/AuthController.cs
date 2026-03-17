using Fintech.IdentityService.Api.Requests;
using Fintech.IdentityService.Api.Responses;
using Fintech.IdentityService.Application.Auth.ConfirmEmailVerification;
using Fintech.IdentityService.Application.Auth.ConfirmPasswordReset;
using Fintech.IdentityService.Application.Auth.IssueToken;
using Fintech.IdentityService.Application.Auth.RefreshAccessToken;
using Fintech.IdentityService.Application.Auth.RequestEmailVerification;
using Fintech.IdentityService.Application.Auth.RequestPasswordReset;
using Fintech.IdentityService.Application.Auth.RevokeRefreshToken;
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
            result.AccessTokenExpiresAtUtc,
            result.RefreshToken,
            result.RefreshTokenExpiresAtUtc,
            result.UserId,
            result.Email));
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<TokenEnvelopeResponse>> Refresh(
        [FromBody] RefreshTokenRequest request,
        [FromServices] RefreshAccessTokenHandler handler)
    {
        var result = await handler.HandleAsync(
            new RefreshAccessTokenCommand(request.RefreshToken),
            HttpContext.RequestAborted);

        if (result is null)
        {
            return Unauthorized();
        }

        return Ok(new TokenEnvelopeResponse(
            result.AccessToken,
            result.AccessTokenExpiresAtUtc,
            result.RefreshToken,
            result.RefreshTokenExpiresAtUtc,
            result.UserId,
            result.Email));
    }

    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke(
        [FromBody] RevokeRefreshTokenRequest request,
        [FromServices] RevokeRefreshTokenHandler handler)
    {
        var revoked = await handler.HandleAsync(
            new RevokeRefreshTokenCommand(request.RefreshToken),
            HttpContext.RequestAborted);

        return revoked ? NoContent() : Unauthorized();
    }

    [HttpPost("password-reset/request")]
    public async Task<IActionResult> RequestPasswordReset(
        [FromBody] RequestPasswordResetRequest request,
        [FromServices] RequestPasswordResetHandler handler)
    {
        await handler.HandleAsync(
            new RequestPasswordResetCommand(request.Email),
            HttpContext.RequestAborted);

        return Accepted();
    }

    [HttpPost("password-reset/confirm")]
    public async Task<IActionResult> ConfirmPasswordReset(
        [FromBody] ConfirmPasswordResetRequest request,
        [FromServices] ConfirmPasswordResetHandler handler)
    {
        var confirmed = await handler.HandleAsync(
            new ConfirmPasswordResetCommand(request.Token, request.NewPassword),
            HttpContext.RequestAborted);

        return confirmed ? NoContent() : Unauthorized();
    }

    [HttpPost("email-verification/request")]
    public async Task<IActionResult> RequestEmailVerification(
        [FromBody] RequestEmailVerificationRequest request,
        [FromServices] RequestEmailVerificationHandler handler)
    {
        await handler.HandleAsync(
            new RequestEmailVerificationCommand(request.Email),
            HttpContext.RequestAborted);

        return Accepted();
    }

    [HttpPost("email-verification/confirm")]
    public async Task<IActionResult> ConfirmEmailVerification(
        [FromBody] ConfirmEmailVerificationRequest request,
        [FromServices] ConfirmEmailVerificationHandler handler)
    {
        var confirmed = await handler.HandleAsync(
            new ConfirmEmailVerificationCommand(request.Token),
            HttpContext.RequestAborted);

        return confirmed ? NoContent() : Unauthorized();
    }
}
