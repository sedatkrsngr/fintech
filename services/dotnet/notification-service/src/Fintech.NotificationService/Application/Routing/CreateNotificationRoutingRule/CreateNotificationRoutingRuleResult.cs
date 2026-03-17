using Fintech.Contracts.Notifications;
using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Routing.CreateNotificationRoutingRule;

public sealed record CreateNotificationRoutingRuleResult(
    Guid Id,
    NotificationMessageType MessageType,
    NotificationChannel Channel,
    Guid ProviderId,
    int Priority,
    bool IsActive,
    DateTime CreatedAtUtc);
