using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Deliveries.CreateSmsDelivery;

public sealed record CreateSmsDeliveryResult(
    Guid NotificationDeliveryId,
    Guid ProviderId,
    NotificationChannel Channel,
    NotificationDeliveryStatus Status,
    DateTime CreatedAtUtc,
    string PhoneNumber,
    string Message);
