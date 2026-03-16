using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Deliveries.CreateSmsDelivery;

public sealed record CreateSmsDeliveryCommand(
    Guid ProviderId,
    string PhoneNumber,
    string Message,
    NotificationChannel Channel = NotificationChannel.Sms);
