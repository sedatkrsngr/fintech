using Fintech.Contracts.Notifications;
using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Domain.Entities;

public sealed class NotificationRoutingRule
{
    private NotificationRoutingRule()
    {
    }

    private NotificationRoutingRule(
        Guid id,
        NotificationMessageType messageType,
        NotificationChannel channel,
        Guid providerId,
        int priority,
        bool isActive,
        DateTime createdAtUtc)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Routing rule id cannot be empty.", nameof(id));
        }

        if (providerId == Guid.Empty)
        {
            throw new ArgumentException("Provider id cannot be empty.", nameof(providerId));
        }

        if (priority < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(priority), "Priority cannot be negative.");
        }

        Id = id;
        MessageType = messageType;
        Channel = channel;
        ProviderId = providerId;
        Priority = priority;
        IsActive = isActive;
        CreatedAtUtc = createdAtUtc;
    }

    public Guid Id { get; private set; }

    public NotificationMessageType MessageType { get; private set; }

    public NotificationChannel Channel { get; private set; }

    public Guid ProviderId { get; private set; }

    public int Priority { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public static NotificationRoutingRule Create(
        NotificationMessageType messageType,
        NotificationChannel channel,
        Guid providerId,
        int priority = 0,
        bool isActive = true)
    {
        return new NotificationRoutingRule(
            Guid.NewGuid(),
            messageType,
            channel,
            providerId,
            priority,
            isActive,
            DateTime.UtcNow);
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
