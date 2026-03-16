using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Deliveries.CreateEmailDelivery;

public sealed record CreateEmailDeliveryResult(
    Guid NotificationDeliveryId,
    Guid ProviderId,
    NotificationChannel Channel,
    NotificationDeliveryStatus Status,
    DateTime CreatedAtUtc,
    string ToEmail,
    string Subject,
    string Body);
