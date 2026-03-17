using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Messages.SendEmailMessage;

public sealed record SendEmailMessageResult(
    Guid NotificationDeliveryId,
    Guid ProviderId,
    NotificationChannel Channel,
    NotificationDeliveryStatus Status,
    DateTime CreatedAtUtc,
    string ToEmail,
    string Subject,
    string Body);
