using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Providers.CreateNotificationProvider;

public sealed record CreateNotificationProviderResult(
    Guid Id,
    string ProviderKey,
    string DisplayName,
    NotificationChannel Channel,
    NotificationProviderType ProviderType,
    bool IsActive,
    DateTime CreatedAtUtc);
