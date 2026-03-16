using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Api.Requests;

public sealed record CreateNotificationProviderRequest(
    string ProviderKey,
    string DisplayName,
    NotificationChannel Channel,
    NotificationProviderType ProviderType,
    bool IsActive);
