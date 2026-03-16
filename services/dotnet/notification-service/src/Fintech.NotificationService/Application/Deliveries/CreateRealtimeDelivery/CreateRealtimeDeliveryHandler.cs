using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Domain.Entities;

namespace Fintech.NotificationService.Application.Deliveries.CreateRealtimeDelivery;

public sealed class CreateRealtimeDeliveryHandler
{
    private readonly INotificationDeliveryRepository _notificationDeliveryRepository;

    public CreateRealtimeDeliveryHandler(INotificationDeliveryRepository notificationDeliveryRepository)
    {
        _notificationDeliveryRepository = notificationDeliveryRepository;
    }

    public async Task<CreateRealtimeDeliveryResult> HandleAsync(
        CreateRealtimeDeliveryCommand command,
        CancellationToken cancellationToken = default)
    {
        var delivery = NotificationDelivery.Create(command.ProviderId, command.Channel);
        var realtimeDelivery = RealtimeDelivery.Create(delivery.Id, command.TargetUserId, command.Payload);

        await _notificationDeliveryRepository.AddRealtimeDeliveryAsync(delivery, realtimeDelivery, cancellationToken);

        return new CreateRealtimeDeliveryResult(
            delivery.Id,
            delivery.ProviderId,
            delivery.Channel,
            delivery.Status,
            delivery.CreatedAtUtc,
            realtimeDelivery.TargetUserId,
            realtimeDelivery.Payload);
    }
}
