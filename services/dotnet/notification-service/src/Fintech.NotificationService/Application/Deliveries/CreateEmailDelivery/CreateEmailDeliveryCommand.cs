using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Deliveries.CreateEmailDelivery;

public sealed record CreateEmailDeliveryCommand(
    Guid ProviderId,
    string ToEmail,
    string Subject,
    string Body,
    NotificationChannel Channel = NotificationChannel.Email);
