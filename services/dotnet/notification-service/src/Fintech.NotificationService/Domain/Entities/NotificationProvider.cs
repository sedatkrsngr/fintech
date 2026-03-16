using Fintech.NotificationService.Domain.Enums;
using Fintech.NotificationService.Domain.Events;
using Fintech.NotificationService.Domain.ValueObjects;

namespace Fintech.NotificationService.Domain.Entities;

public sealed class NotificationProvider
{
    private NotificationProvider()
    {
    }

    private NotificationProvider(
        Guid id,
        ProviderKey providerKey,
        string displayName,
        NotificationChannel channel,
        NotificationProviderType providerType,
        bool isActive,
        DateTime createdAtUtc)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Provider id cannot be empty.", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(displayName))
        {
            throw new ArgumentException("Display name is required.", nameof(displayName));
        }

        Id = id;
        ProviderKey = providerKey;
        DisplayName = displayName.Trim();
        Channel = channel;
        ProviderType = providerType;
        IsActive = isActive;
        CreatedAtUtc = createdAtUtc;
    }

    public Guid Id { get; private set; }

    public ProviderKey ProviderKey { get; private set; }

    public string DisplayName { get; private set; } = string.Empty;

    public NotificationChannel Channel { get; private set; }

    public NotificationProviderType ProviderType { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAtUtc { get; private set; }

    public NotificationProviderCreated? CreatedEvent { get; private set; }

    public static NotificationProvider Create(
        string providerKey,
        string displayName,
        NotificationChannel channel,
        NotificationProviderType providerType,
        bool isActive = true)
    {
        var createdAtUtc = DateTime.UtcNow;

        var provider = new NotificationProvider(
            Guid.NewGuid(),
            ProviderKey.Create(providerKey),
            displayName,
            channel,
            providerType,
            isActive,
            createdAtUtc);

        provider.CreatedEvent = new NotificationProviderCreated(
            provider.Id,
            provider.ProviderKey,
            provider.Channel,
            provider.ProviderType,
            createdAtUtc);

        return provider;
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
