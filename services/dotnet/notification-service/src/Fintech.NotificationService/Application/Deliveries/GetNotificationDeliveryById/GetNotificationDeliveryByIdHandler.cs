using Fintech.NotificationService.Application.Abstractions;

namespace Fintech.NotificationService.Application.Deliveries.GetNotificationDeliveryById;

public sealed class GetNotificationDeliveryByIdHandler
{
    private readonly INotificationDeliveryRepository _notificationDeliveryRepository;

    public GetNotificationDeliveryByIdHandler(INotificationDeliveryRepository notificationDeliveryRepository)
    {
        _notificationDeliveryRepository = notificationDeliveryRepository;
    }

    public async Task<GetNotificationDeliveryByIdResult?> HandleAsync(
        GetNotificationDeliveryByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var delivery = await _notificationDeliveryRepository.GetByIdAsync(query.NotificationDeliveryId, cancellationToken);

        if (delivery is null)
        {
            return null;
        }

        var emailDelivery = await _notificationDeliveryRepository.GetEmailDeliveryByNotificationDeliveryIdAsync(
            delivery.Id,
            cancellationToken);
        var smsDelivery = await _notificationDeliveryRepository.GetSmsDeliveryByNotificationDeliveryIdAsync(
            delivery.Id,
            cancellationToken);
        var realtimeDelivery = await _notificationDeliveryRepository.GetRealtimeDeliveryByNotificationDeliveryIdAsync(
            delivery.Id,
            cancellationToken);

        return new GetNotificationDeliveryByIdResult(
            delivery.Id,
            delivery.ProviderId,
            delivery.Channel,
            delivery.Status,
            delivery.CreatedAtUtc,
            emailDelivery?.ToEmail.Value,
            emailDelivery?.Subject,
            emailDelivery?.Body,
            smsDelivery?.PhoneNumber.Value,
            smsDelivery?.Message,
            realtimeDelivery?.TargetUserId,
            realtimeDelivery?.Payload);
    }
}
