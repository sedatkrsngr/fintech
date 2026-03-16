using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Domain.Entities;

namespace Fintech.NotificationService.Application.Routing.CreateNotificationRoutingRule;

public sealed class CreateNotificationRoutingRuleHandler
{
    private readonly INotificationRoutingRuleRepository _notificationRoutingRuleRepository;

    public CreateNotificationRoutingRuleHandler(INotificationRoutingRuleRepository notificationRoutingRuleRepository)
    {
        _notificationRoutingRuleRepository = notificationRoutingRuleRepository;
    }

    public async Task<CreateNotificationRoutingRuleResult> HandleAsync(
        CreateNotificationRoutingRuleCommand command,
        CancellationToken cancellationToken = default)
    {
        var routingRule = NotificationRoutingRule.Create(
            command.MessageType,
            command.Channel,
            command.ProviderId,
            command.Priority,
            command.IsActive);

        await _notificationRoutingRuleRepository.AddAsync(routingRule, cancellationToken);

        return new CreateNotificationRoutingRuleResult(
            routingRule.Id,
            routingRule.MessageType,
            routingRule.Channel,
            routingRule.ProviderId,
            routingRule.Priority,
            routingRule.IsActive,
            routingRule.CreatedAtUtc);
    }
}
