using Fintech.NotificationService.Domain.ValueObjects;

namespace Fintech.NotificationService.Domain.Entities;

public sealed class EmailDelivery
{
    private EmailDelivery()
    {
    }

    private EmailDelivery(
        Guid notificationDeliveryId,
        RecipientEmail toEmail,
        string subject,
        string body)
    {
        if (notificationDeliveryId == Guid.Empty)
        {
            throw new ArgumentException("Notification delivery id cannot be empty.", nameof(notificationDeliveryId));
        }

        if (string.IsNullOrWhiteSpace(subject))
        {
            throw new ArgumentException("Email subject is required.", nameof(subject));
        }

        if (string.IsNullOrWhiteSpace(body))
        {
            throw new ArgumentException("Email body is required.", nameof(body));
        }

        NotificationDeliveryId = notificationDeliveryId;
        ToEmail = toEmail;
        Subject = subject.Trim();
        Body = body.Trim();
    }

    public Guid NotificationDeliveryId { get; private set; }

    public RecipientEmail ToEmail { get; private set; }

    public string Subject { get; private set; } = string.Empty;

    public string Body { get; private set; } = string.Empty;

    public static EmailDelivery Create(
        Guid notificationDeliveryId,
        string toEmail,
        string subject,
        string body)
    {
        return new EmailDelivery(notificationDeliveryId, RecipientEmail.Create(toEmail), subject, body);
    }
}
