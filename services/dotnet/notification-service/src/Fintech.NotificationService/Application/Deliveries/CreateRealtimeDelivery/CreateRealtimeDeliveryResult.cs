using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Deliveries.CreateRealtimeDelivery;

public sealed record CreateRealtimeDeliveryResult(
    Guid NotificationDeliveryId,
    Guid ProviderId,
    NotificationChannel Channel,
    NotificationDeliveryStatus Status,
    DateTime CreatedAtUtc,
    string TargetUserId,
    string Payload);
