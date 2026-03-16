using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Domain.Entities;

namespace Fintech.NotificationService.Application.Deliveries.CreateEmailDelivery;

public sealed class CreateEmailDeliveryHandler
{
    private readonly INotificationDeliveryRepository _notificationDeliveryRepository;

    public CreateEmailDeliveryHandler(INotificationDeliveryRepository notificationDeliveryRepository)
    {
        _notificationDeliveryRepository = notificationDeliveryRepository;
    }

    public async Task<CreateEmailDeliveryResult> HandleAsync(
        CreateEmailDeliveryCommand command,
        CancellationToken cancellationToken = default)
    {
        var delivery = NotificationDelivery.Create(command.ProviderId, command.Channel);
        var emailDelivery = EmailDelivery.Create(delivery.Id, command.ToEmail, command.Subject, command.Body);

        await _notificationDeliveryRepository.AddEmailDeliveryAsync(delivery, emailDelivery, cancellationToken);

        return new CreateEmailDeliveryResult(
            delivery.Id,
            delivery.ProviderId,
            delivery.Channel,
            delivery.Status,
            delivery.CreatedAtUtc,
            emailDelivery.ToEmail.Value,
            emailDelivery.Subject,
            emailDelivery.Body);
    }
}
