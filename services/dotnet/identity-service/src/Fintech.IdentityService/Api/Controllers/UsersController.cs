using Fintech.IdentityService.Api.Requests;
using Fintech.IdentityService.Api.Responses;
using Fintech.IdentityService.Application.Users.GetUserById;
using Fintech.IdentityService.Application.Users.RegisterUser;
using Fintech.IdentityService.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.IdentityService.Api.Controllers;

[ApiController]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<RegisterUserResponse>> GetById(
        Guid userId,
        [FromServices] GetUserByIdHandler handler)
    {
        var query = new GetUserByIdQuery(UserId.From(userId));
        var result = await handler.HandleAsync(query, HttpContext.RequestAborted);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(new RegisterUserResponse(result.UserId, result.Email, result.CreatedAtUtc));
    }

    [HttpPost]
    public async Task<ActionResult<RegisterUserResponse>> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterUserHandler handler)
    {
        var command = new RegisterUserCommand(request.Email);
        var result = await handler.HandleAsync(command, HttpContext.RequestAborted);

        var response = new RegisterUserResponse(result.UserId, result.Email, result.CreatedAtUtc);

        return CreatedAtAction(nameof(GetById), new { userId = response.UserId }, response);
    }
}
