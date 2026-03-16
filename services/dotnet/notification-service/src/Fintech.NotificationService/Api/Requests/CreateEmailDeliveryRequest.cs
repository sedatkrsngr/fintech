namespace Fintech.NotificationService.Api.Requests;

public sealed record CreateEmailDeliveryRequest(
    Guid ProviderId,
    string ToEmail,
    string Subject,
    string Body);
