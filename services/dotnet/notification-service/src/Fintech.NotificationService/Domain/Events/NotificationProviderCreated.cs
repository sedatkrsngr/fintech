using Fintech.NotificationService.Domain.Enums;
using Fintech.NotificationService.Domain.ValueObjects;

namespace Fintech.NotificationService.Domain.Events;

public sealed record NotificationProviderCreated(
    Guid Id,
    ProviderKey ProviderKey,
    NotificationChannel Channel,
    NotificationProviderType ProviderType,
    DateTime OccurredAtUtc);
