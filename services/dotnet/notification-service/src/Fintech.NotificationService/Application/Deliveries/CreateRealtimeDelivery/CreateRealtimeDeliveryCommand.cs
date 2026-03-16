using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Deliveries.CreateRealtimeDelivery;

public sealed record CreateRealtimeDeliveryCommand(
    Guid ProviderId,
    string TargetUserId,
    string Payload,
    NotificationChannel Channel = NotificationChannel.Realtime);
