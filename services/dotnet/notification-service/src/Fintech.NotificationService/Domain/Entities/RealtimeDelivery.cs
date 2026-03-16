namespace Fintech.NotificationService.Domain.Entities;

public sealed class RealtimeDelivery
{
    private RealtimeDelivery()
    {
    }

    private RealtimeDelivery(
        Guid notificationDeliveryId,
        string targetUserId,
        string payload)
    {
        if (notificationDeliveryId == Guid.Empty)
        {
            throw new ArgumentException("Notification delivery id cannot be empty.", nameof(notificationDeliveryId));
        }

        if (string.IsNullOrWhiteSpace(targetUserId))
        {
            throw new ArgumentException("Target user id is required.", nameof(targetUserId));
        }

        if (string.IsNullOrWhiteSpace(payload))
        {
            throw new ArgumentException("Realtime payload is required.", nameof(payload));
        }

        NotificationDeliveryId = notificationDeliveryId;
        TargetUserId = targetUserId.Trim();
        Payload = payload.Trim();
    }

    public Guid NotificationDeliveryId { get; private set; }

    public string TargetUserId { get; private set; } = string.Empty;

    public string Payload { get; private set; } = string.Empty;

    public static RealtimeDelivery Create(
        Guid notificationDeliveryId,
        string targetUserId,
        string payload)
    {
        return new RealtimeDelivery(notificationDeliveryId, targetUserId, payload);
    }
}
