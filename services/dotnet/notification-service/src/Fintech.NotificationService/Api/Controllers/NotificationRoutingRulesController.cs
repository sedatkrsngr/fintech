using Fintech.NotificationService.Api.Requests;
using Fintech.NotificationService.Api.Responses;
using Fintech.NotificationService.Application.Routing.CreateNotificationRoutingRule;
using Fintech.NotificationService.Application.Routing.GetNotificationRoutingRuleById;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.NotificationService.Api.Controllers;

[ApiController]
[Route("api/notification-routing-rules")]
public sealed class NotificationRoutingRulesController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<NotificationRoutingRuleResponse>> Create(
        [FromBody] CreateNotificationRoutingRuleRequest request,
        [FromServices] CreateNotificationRoutingRuleHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(
            new CreateNotificationRoutingRuleCommand(
                request.MessageType,
                request.Channel,
                request.ProviderId,
                request.Priority,
                request.IsActive),
            cancellationToken);

        var response = new NotificationRoutingRuleResponse(
            result.Id,
            result.MessageType,
            result.Channel,
            result.ProviderId,
            result.Priority,
            result.IsActive,
            result.CreatedAtUtc);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NotificationRoutingRuleResponse>> GetById(
        Guid id,
        [FromServices] GetNotificationRoutingRuleByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new GetNotificationRoutingRuleByIdQuery(id), cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(new NotificationRoutingRuleResponse(
            result.Id,
            result.MessageType,
            result.Channel,
            result.ProviderId,
            result.Priority,
            result.IsActive,
            result.CreatedAtUtc));
    }
}
