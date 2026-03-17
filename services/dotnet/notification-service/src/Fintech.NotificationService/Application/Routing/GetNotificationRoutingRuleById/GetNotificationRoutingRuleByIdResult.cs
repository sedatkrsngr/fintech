using Fintech.Contracts.Notifications;
using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Application.Routing.GetNotificationRoutingRuleById;

public sealed record GetNotificationRoutingRuleByIdResult(
    Guid Id,
    NotificationMessageType MessageType,
    NotificationChannel Channel,
    Guid ProviderId,
    int Priority,
    bool IsActive,
    DateTime CreatedAtUtc);
