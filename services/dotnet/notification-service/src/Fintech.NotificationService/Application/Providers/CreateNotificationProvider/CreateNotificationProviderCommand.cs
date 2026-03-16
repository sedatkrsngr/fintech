using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Providers.CreateNotificationProvider;

public sealed record CreateNotificationProviderCommand(
    string ProviderKey,
    string DisplayName,
    NotificationChannel Channel,
    NotificationProviderType ProviderType,
    bool IsActive);
