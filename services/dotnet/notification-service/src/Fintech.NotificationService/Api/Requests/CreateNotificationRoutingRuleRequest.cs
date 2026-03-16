using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Api.Requests;

public sealed record CreateNotificationRoutingRuleRequest(
    NotificationMessageType MessageType,
    NotificationChannel Channel,
    Guid ProviderId,
    int Priority,
    bool IsActive);
