using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Domain.Entities;

namespace Fintech.NotificationService.Application.Deliveries.CreateRealtimeDelivery;

public sealed class CreateRealtimeDeliveryHandler
{
    private readonly INotificationDeliveryRepository _notificationDeliveryRepository;
    private readonly INotificationProviderResolver _notificationProviderResolver;

    public CreateRealtimeDeliveryHandler(
        INotificationDeliveryRepository notificationDeliveryRepository,
        INotificationProviderResolver notificationProviderResolver)
    {
        _notificationDeliveryRepository = notificationDeliveryRepository;
        _notificationProviderResolver = notificationProviderResolver;
    }

    public async Task<CreateRealtimeDeliveryResult> HandleAsync(
        CreateRealtimeDeliveryCommand command,
        CancellationToken cancellationToken = default)
    {
        var provider = await _notificationProviderResolver.ResolveRealtimeProviderAsync(
            command.ProviderId,
            cancellationToken);

        var delivery = NotificationDelivery.Create(command.ProviderId, command.Channel);
        var realtimeDelivery = RealtimeDelivery.Create(delivery.Id, command.TargetUserId, command.Payload);

        await provider.SendAsync(
            command.ProviderId,
            realtimeDelivery.TargetUserId,
            realtimeDelivery.Payload,
            cancellationToken);

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
