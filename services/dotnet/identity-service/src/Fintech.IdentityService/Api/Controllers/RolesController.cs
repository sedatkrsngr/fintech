using Fintech.IdentityService.Api.Requests;
using Fintech.IdentityService.Api.Responses;
using Fintech.IdentityService.Application.Roles.CreateRole;
using Fintech.IdentityService.Application.Roles.GetRoleById;
using Fintech.IdentityService.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.IdentityService.Api.Controllers;

[ApiController]
[Route("api/roles")]
public sealed class RolesController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<RoleResponse>> Create(
        [FromBody] CreateRoleRequest request,
        [FromServices] CreateRoleHandler handler)
    {
        var result = await handler.HandleAsync(new CreateRoleCommand(request.Name), HttpContext.RequestAborted);
        var response = new RoleResponse(result.RoleId, result.Name, result.CreatedAtUtc);

        return CreatedAtAction(nameof(GetById), new { roleId = response.RoleId }, response);
    }

    [HttpGet("{roleId:guid}")]
    public async Task<ActionResult<RoleResponse>> GetById(
        Guid roleId,
        [FromServices] GetRoleByIdHandler handler)
    {
        var result = await handler.HandleAsync(new GetRoleByIdQuery(RoleId.From(roleId)), HttpContext.RequestAborted);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(new RoleResponse(result.RoleId, result.Name, result.CreatedAtUtc));
    }
}
