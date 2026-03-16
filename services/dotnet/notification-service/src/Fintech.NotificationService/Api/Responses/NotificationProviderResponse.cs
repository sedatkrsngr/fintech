using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Api.Responses;

public sealed record NotificationProviderResponse(
    Guid Id,
    string ProviderKey,
    string DisplayName,
    NotificationChannel Channel,
    NotificationProviderType ProviderType,
    bool IsActive,
    DateTime CreatedAtUtc);
