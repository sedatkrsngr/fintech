using Fintech.IdentityService.Api.Requests;
using Fintech.IdentityService.Api.Responses;
using Fintech.IdentityService.Application.Permissions.CreatePermission;
using Fintech.IdentityService.Application.Permissions.GetPermissionById;
using Fintech.IdentityService.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.IdentityService.Api.Controllers;

[ApiController]
[Route("api/permissions")]
public sealed class PermissionsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PermissionResponse>> Create(
        [FromBody] CreatePermissionRequest request,
        [FromServices] CreatePermissionHandler handler)
    {
        var result = await handler.HandleAsync(new CreatePermissionCommand(request.Code), HttpContext.RequestAborted);
        var response = new PermissionResponse(result.PermissionId, result.Code, result.CreatedAtUtc);

        return CreatedAtAction(nameof(GetById), new { permissionId = response.PermissionId }, response);
    }

    [HttpGet("{permissionId:guid}")]
    public async Task<ActionResult<PermissionResponse>> GetById(
        Guid permissionId,
        [FromServices] GetPermissionByIdHandler handler)
    {
        var result = await handler.HandleAsync(
            new GetPermissionByIdQuery(PermissionId.From(permissionId)),
            HttpContext.RequestAborted);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(new PermissionResponse(result.PermissionId, result.Code, result.CreatedAtUtc));
    }
}
