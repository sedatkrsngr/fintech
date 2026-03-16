namespace Fintech.NotificationService.Api.Requests;

public sealed record CreateSmsDeliveryRequest(
    Guid ProviderId,
    string PhoneNumber,
    string Message);
