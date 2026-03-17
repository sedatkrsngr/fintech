using Fintech.IdentityService.Api.Requests;
using Fintech.IdentityService.Api.Responses;
using Fintech.IdentityService.Application.Groups.CreateGroup;
using Fintech.IdentityService.Application.Groups.GetGroupById;
using Fintech.IdentityService.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.IdentityService.Api.Controllers;

[ApiController]
[Route("api/groups")]
public sealed class GroupsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<GroupResponse>> Create(
        [FromBody] CreateGroupRequest request,
        [FromServices] CreateGroupHandler handler)
    {
        var result = await handler.HandleAsync(new CreateGroupCommand(request.Name), HttpContext.RequestAborted);
        var response = new GroupResponse(result.GroupId, result.Name, result.CreatedAtUtc);

        return CreatedAtAction(nameof(GetById), new { groupId = response.GroupId }, response);
    }

    [HttpGet("{groupId:guid}")]
    public async Task<ActionResult<GroupResponse>> GetById(
        Guid groupId,
        [FromServices] GetGroupByIdHandler handler)
    {
        var result = await handler.HandleAsync(new GetGroupByIdQuery(GroupId.From(groupId)), HttpContext.RequestAborted);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(new GroupResponse(result.GroupId, result.Name, result.CreatedAtUtc));
    }
}
