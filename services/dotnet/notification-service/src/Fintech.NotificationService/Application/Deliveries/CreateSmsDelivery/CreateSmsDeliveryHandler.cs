using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Domain.Entities;

namespace Fintech.NotificationService.Application.Deliveries.CreateSmsDelivery;

public sealed class CreateSmsDeliveryHandler
{
    private readonly INotificationDeliveryRepository _notificationDeliveryRepository;
    private readonly INotificationProviderResolver _notificationProviderResolver;

    public CreateSmsDeliveryHandler(
        INotificationDeliveryRepository notificationDeliveryRepository,
        INotificationProviderResolver notificationProviderResolver)
    {
        _notificationDeliveryRepository = notificationDeliveryRepository;
        _notificationProviderResolver = notificationProviderResolver;
    }

    public async Task<CreateSmsDeliveryResult> HandleAsync(
        CreateSmsDeliveryCommand command,
        CancellationToken cancellationToken = default)
    {
        var provider = await _notificationProviderResolver.ResolveSmsProviderAsync(
            command.ProviderId,
            cancellationToken);

        var delivery = NotificationDelivery.Create(command.ProviderId, command.Channel);
        var smsDelivery = SmsDelivery.Create(delivery.Id, command.PhoneNumber, command.Message);

        await provider.SendAsync(
            command.ProviderId,
            smsDelivery.PhoneNumber.Value,
            smsDelivery.Message,
            cancellationToken);

        await _notificationDeliveryRepository.AddSmsDeliveryAsync(delivery, smsDelivery, cancellationToken);

        return new CreateSmsDeliveryResult(
            delivery.Id,
            delivery.ProviderId,
            delivery.Channel,
            delivery.Status,
            delivery.CreatedAtUtc,
            smsDelivery.PhoneNumber.Value,
            smsDelivery.Message);
    }
}
