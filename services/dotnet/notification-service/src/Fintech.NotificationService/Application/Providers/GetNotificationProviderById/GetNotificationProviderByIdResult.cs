using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Providers.GetNotificationProviderById;

public sealed record GetNotificationProviderByIdResult(
    Guid Id,
    string ProviderKey,
    string DisplayName,
    NotificationChannel Channel,
    NotificationProviderType ProviderType,
    bool IsActive,
    DateTime CreatedAtUtc);
