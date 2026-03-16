using Fintech.NotificationService.Domain.ValueObjects;

namespace Fintech.NotificationService.Domain.Entities;

public sealed class SmsDelivery
{
    private SmsDelivery()
    {
    }

    private SmsDelivery(
        Guid notificationDeliveryId,
        PhoneNumber phoneNumber,
        string message)
    {
        if (notificationDeliveryId == Guid.Empty)
        {
            throw new ArgumentException("Notification delivery id cannot be empty.", nameof(notificationDeliveryId));
        }

        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException("SMS message is required.", nameof(message));
        }

        NotificationDeliveryId = notificationDeliveryId;
        PhoneNumber = phoneNumber;
        Message = message.Trim();
    }

    public Guid NotificationDeliveryId { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public string Message { get; private set; } = string.Empty;

    public static SmsDelivery Create(
        Guid notificationDeliveryId,
        string phoneNumber,
        string message)
    {
        return new SmsDelivery(notificationDeliveryId, PhoneNumber.Create(phoneNumber), message);
    }
}
