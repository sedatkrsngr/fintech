using Fintech.NotificationService.Api.Requests;
using Fintech.NotificationService.Api.Responses;
using Fintech.NotificationService.Application.Providers.CreateNotificationProvider;
using Fintech.NotificationService.Application.Providers.GetNotificationProviderById;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.NotificationService.Api.Controllers;

[ApiController]
[Route("api/notification-providers")]
public sealed class NotificationProvidersController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<NotificationProviderResponse>> Create(
        [FromBody] CreateNotificationProviderRequest request,
        [FromServices] CreateNotificationProviderHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(
            new CreateNotificationProviderCommand(
                request.ProviderKey,
                request.DisplayName,
                request.Channel,
                request.ProviderType,
                request.IsActive),
            cancellationToken);

        var response = new NotificationProviderResponse(
            result.Id,
            result.ProviderKey,
            result.DisplayName,
            result.Channel,
            result.ProviderType,
            result.IsActive,
            result.CreatedAtUtc);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NotificationProviderResponse>> GetById(
        Guid id,
        [FromServices] GetNotificationProviderByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new GetNotificationProviderByIdQuery(id), cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(new NotificationProviderResponse(
            result.Id,
            result.ProviderKey,
            result.DisplayName,
            result.Channel,
            result.ProviderType,
            result.IsActive,
            result.CreatedAtUtc));
    }
}
