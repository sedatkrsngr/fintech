using Fintech.NotificationService.Api.Requests;
using Fintech.NotificationService.Api.Responses;
using Fintech.NotificationService.Application.Messages.SendEmailMessage;
using Microsoft.AspNetCore.Mvc;

namespace Fintech.NotificationService.Api.Controllers;

[ApiController]
[Route("api/notification-messages")]
public sealed class NotificationMessagesController : ControllerBase
{
    [HttpPost("email")]
    public async Task<ActionResult<NotificationDeliveryResponse>> SendEmail(
        [FromBody] SendEmailMessageRequest request,
        [FromServices] SendEmailMessageHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(
            new SendEmailMessageCommand(
                request.MessageType,
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

        return Ok(response);
    }
}
