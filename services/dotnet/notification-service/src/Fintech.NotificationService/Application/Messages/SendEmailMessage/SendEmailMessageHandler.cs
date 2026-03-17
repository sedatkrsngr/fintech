using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Application.Deliveries.CreateEmailDelivery;
using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Messages.SendEmailMessage;

public sealed class SendEmailMessageHandler
{
    private readonly INotificationRoutingRuleRepository _notificationRoutingRuleRepository;
    private readonly CreateEmailDeliveryHandler _createEmailDeliveryHandler;

    public SendEmailMessageHandler(
        INotificationRoutingRuleRepository notificationRoutingRuleRepository,
        CreateEmailDeliveryHandler createEmailDeliveryHandler)
    {
        _notificationRoutingRuleRepository = notificationRoutingRuleRepository;
        _createEmailDeliveryHandler = createEmailDeliveryHandler;
    }

    public async Task<SendEmailMessageResult> HandleAsync(
        SendEmailMessageCommand command,
        CancellationToken cancellationToken = default)
    {
        var routingRule = await _notificationRoutingRuleRepository.GetBestActiveRuleAsync(
            command.MessageType,
            NotificationChannel.Email,
            cancellationToken);

        if (routingRule is null)
        {
            throw new InvalidOperationException(
                $"No active email routing rule found for message type '{command.MessageType}'.");
        }

        var result = await _createEmailDeliveryHandler.HandleAsync(
            new CreateEmailDeliveryCommand(
                routingRule.ProviderId,
                command.ToEmail,
                command.Subject,
                command.Body),
            cancellationToken);

        return new SendEmailMessageResult(
            result.NotificationDeliveryId,
            result.ProviderId,
            result.Channel,
            result.Status,
            result.CreatedAtUtc,
            result.ToEmail,
            result.Subject,
            result.Body);
    }
}
