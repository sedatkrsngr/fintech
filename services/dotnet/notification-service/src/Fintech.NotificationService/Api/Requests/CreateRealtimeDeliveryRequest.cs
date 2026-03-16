namespace Fintech.NotificationService.Api.Requests;

public sealed record CreateRealtimeDeliveryRequest(
    Guid ProviderId,
    string TargetUserId,
    string Payload);
