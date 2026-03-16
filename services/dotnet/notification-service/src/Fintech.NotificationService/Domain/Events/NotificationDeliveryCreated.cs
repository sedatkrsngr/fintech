using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Domain.Events;

public sealed record NotificationDeliveryCreated(
    Guid Id,
    Guid ProviderId,
    NotificationChannel Channel,
    DateTime OccurredAtUtc);
