using Fintech.IdentityService.Api.Requests;
using Fintech.IdentityService.Application.Assignments.AssignGroupToUser;
using Fintech.IdentityService.Application.Assignments.AssignPermissionToRole;
using Fintech.IdentityService.Application.Assignments.AssignRoleToUser;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.IdentityService.Api.Controllers;

[ApiController]
[Route("api/assignments")]
public sealed class AssignmentsController : ControllerBase
{
    [HttpPost("user-role")]
    public async Task<IActionResult> AssignRoleToUser(
        [FromBody] AssignRoleToUserRequest request,
        [FromServices] AssignRoleToUserHandler handler)
    {
        await handler.HandleAsync(new AssignRoleToUserCommand(request.UserId, request.RoleId), HttpContext.RequestAborted);
        return NoContent();
    }

    [HttpPost("user-group")]
    public async Task<IActionResult> AssignGroupToUser(
        [FromBody] AssignGroupToUserRequest request,
        [FromServices] AssignGroupToUserHandler handler)
    {
        await handler.HandleAsync(new AssignGroupToUserCommand(request.UserId, request.GroupId), HttpContext.RequestAborted);
        return NoContent();
    }

    [HttpPost("role-permission")]
    public async Task<IActionResult> AssignPermissionToRole(
        [FromBody] AssignPermissionToRoleRequest request,
        [FromServices] AssignPermissionToRoleHandler handler)
    {
        await handler.HandleAsync(new AssignPermissionToRoleCommand(request.RoleId, request.PermissionId), HttpContext.RequestAborted);
        return NoContent();
    }
}
