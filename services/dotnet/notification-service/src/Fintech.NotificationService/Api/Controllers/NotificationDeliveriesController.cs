using Fintech.NotificationService.Api.Requests;
using Fintech.NotificationService.Api.Responses;
using Fintech.NotificationService.Application.Deliveries.CreateEmailDelivery;
using Fintech.NotificationService.Application.Deliveries.CreateSmsDelivery;
using Fintech.NotificationService.Application.Deliveries.CreateRealtimeDelivery;
using Fintech.NotificationService.Application.Deliveries.GetNotificationDeliveryById;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.NotificationService.Api.Controllers;

[ApiController]
[Route("api/notification-deliveries")]
public sealed class NotificationDeliveriesController : ControllerBase
{
    [HttpPost("email")]
    public async Task<ActionResult<NotificationDeliveryResponse>> CreateEmail(
        [FromBody] CreateEmailDeliveryRequest request,
        [FromServices] CreateEmailDeliveryHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(
            new CreateEmailDeliveryCommand(
                request.ProviderId,
                request.ToEmail,
                request.Subject,
                request.Body),
            cancellationToken);

        var response = new NotificationDeliveryResponse(
            result.NotificationDeliveryId,
            result.ProviderId,
            result.Channel,
            result.Status,
            result.CreatedAtUtc,
            result.ToEmail,
            result.Subject,
            result.Body,
            null,
            null,
            null,
            null);

        return CreatedAtAction(nameof(GetById), new { id = response.NotificationDeliveryId }, response);
    }

    [HttpPost("sms")]
    public async Task<ActionResult<NotificationDeliveryResponse>> CreateSms(
        [FromBody] CreateSmsDeliveryRequest request,
        [FromServices] CreateSmsDeliveryHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(
            new CreateSmsDeliveryCommand(
                request.ProviderId,
                request.PhoneNumber,
                request.Message),
            cancellationToken);

        var response = new NotificationDeliveryResponse(
            result.NotificationDeliveryId,
            result.ProviderId,
            result.Channel,
            result.Status,
            result.CreatedAtUtc,
            null,
            null,
            null,
            result.PhoneNumber,
            result.Message,
            null,
            null);

        return CreatedAtAction(nameof(GetById), new { id = response.NotificationDeliveryId }, response);
    }

    [HttpPost("realtime")]
    public async Task<ActionResult<NotificationDeliveryResponse>> CreateRealtime(
        [FromBody] CreateRealtimeDeliveryRequest request,
        [FromServices] CreateRealtimeDeliveryHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(
            new CreateRealtimeDeliveryCommand(
                request.ProviderId,
                request.TargetUserId,
                request.Payload),
            cancellationToken);

        var response = new NotificationDeliveryResponse(
            result.NotificationDeliveryId,
            result.ProviderId,
            result.Channel,
            result.Status,
            result.CreatedAtUtc,
            null,
            null,
            null,
            null,
            null,
            result.TargetUserId,
            result.Payload);

        return CreatedAtAction(nameof(GetById), new { id = response.NotificationDeliveryId }, response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NotificationDeliveryResponse>> GetById(
        Guid id,
        [FromServices] GetNotificationDeliveryByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new GetNotificationDeliveryByIdQuery(id), cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(new NotificationDeliveryResponse(
            result.NotificationDeliveryId,
            result.ProviderId,
            result.Channel,
            result.Status,
            result.CreatedAtUtc,
            result.ToEmail,
            result.Subject,
            result.Body,
            result.PhoneNumber,
            result.Message,
            result.TargetUserId,
            result.Payload));
    }
}
