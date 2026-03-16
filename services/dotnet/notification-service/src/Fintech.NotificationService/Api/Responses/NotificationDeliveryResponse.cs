using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Api.Responses;

public sealed record NotificationDeliveryResponse(
    Guid NotificationDeliveryId,
    Guid ProviderId,
    NotificationChannel Channel,
    NotificationDeliveryStatus Status,
    DateTime CreatedAtUtc,
    string? ToEmail,
    string? Subject,
    string? Body,
    string? PhoneNumber,
    string? Message,
    string? TargetUserId,
    string? Payload);
