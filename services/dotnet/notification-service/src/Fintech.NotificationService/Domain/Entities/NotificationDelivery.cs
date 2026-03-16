using Fintech.NotificationService.Domain.Enums;
using Fintech.NotificationService.Domain.Events;

namespace Fintech.NotificationService.Domain.Entities;

public sealed class NotificationDelivery
{
    private NotificationDelivery()
    {
    }

    private NotificationDelivery(
        Guid id,
        Guid providerId,
        NotificationChannel channel,
        NotificationDeliveryStatus status,
        DateTime createdAtUtc)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Delivery id cannot be empty.", nameof(id));
        }

        if (providerId == Guid.Empty)
        {
            throw new ArgumentException("Provider id cannot be empty.", nameof(providerId));
        }

        Id = id;
        ProviderId = providerId;
        Channel = channel;
        Status = status;
        CreatedAtUtc = createdAtUtc;
    }

    public Guid Id { get; private set; }

    public Guid ProviderId { get; private set; }

    public NotificationChannel Channel { get; private set; }

    public NotificationDeliveryStatus Status { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public NotificationDeliveryCreated? CreatedEvent { get; private set; }

    public static NotificationDelivery Create(
        Guid providerId,
        NotificationChannel channel)
    {
        var createdAtUtc = DateTime.UtcNow;

        var delivery = new NotificationDelivery(
            Guid.NewGuid(),
            providerId,
            channel,
            NotificationDeliveryStatus.Pending,
            createdAtUtc);

        delivery.CreatedEvent = new NotificationDeliveryCreated(
            delivery.Id,
            delivery.ProviderId,
            delivery.Channel,
            createdAtUtc);

        return delivery;
    }

    public void MarkSent()
    {
        Status = NotificationDeliveryStatus.Sent;
    }

    public void MarkFailed()
    {
        Status = NotificationDeliveryStatus.Failed;
    }
}
