using Fintech.Contracts.Notifications;
using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Api.Responses;

public sealed record NotificationRoutingRuleResponse(
    Guid Id,
    NotificationMessageType MessageType,
    NotificationChannel Channel,
    Guid ProviderId,
    int Priority,
    bool IsActive,
    DateTime CreatedAtUtc);
