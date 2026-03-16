using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Routing.CreateNotificationRoutingRule;

public sealed record CreateNotificationRoutingRuleCommand(
    NotificationMessageType MessageType,
    NotificationChannel Channel,
    Guid ProviderId,
    int Priority,
    bool IsActive);
