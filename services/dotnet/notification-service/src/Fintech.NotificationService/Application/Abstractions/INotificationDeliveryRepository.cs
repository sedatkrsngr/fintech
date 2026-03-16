using Fintech.NotificationService.Domain.Entities;

namespace Fintech.NotificationService.Application.Abstractions;

public interface INotificationDeliveryRepository
{
    Task AddEmailDeliveryAsync(
        NotificationDelivery delivery,
        EmailDelivery emailDelivery,
        CancellationToken cancellationToken = default);

    Task AddSmsDeliveryAsync(
        NotificationDelivery delivery,
        SmsDelivery smsDelivery,
        CancellationToken cancellationToken = default);

    Task AddRealtimeDeliveryAsync(
        NotificationDelivery delivery,
        RealtimeDelivery realtimeDelivery,
        CancellationToken cancellationToken = default);

    Task<NotificationDelivery?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<EmailDelivery?> GetEmailDeliveryByNotificationDeliveryIdAsync(
        Guid notificationDeliveryId,
        CancellationToken cancellationToken = default);

    Task<SmsDelivery?> GetSmsDeliveryByNotificationDeliveryIdAsync(
        Guid notificationDeliveryId,
        CancellationToken cancellationToken = default);

    Task<RealtimeDelivery?> GetRealtimeDeliveryByNotificationDeliveryIdAsync(
        Guid notificationDeliveryId,
        CancellationToken cancellationToken = default);
}
