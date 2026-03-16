using Fintech.NotificationService.Application.Abstractions;

namespace Fintech.NotificationService.Application.Routing.GetNotificationRoutingRuleById;

public sealed class GetNotificationRoutingRuleByIdHandler
{
    private readonly INotificationRoutingRuleRepository _notificationRoutingRuleRepository;

    public GetNotificationRoutingRuleByIdHandler(INotificationRoutingRuleRepository notificationRoutingRuleRepository)
    {
        _notificationRoutingRuleRepository = notificationRoutingRuleRepository;
    }

    public async Task<GetNotificationRoutingRuleByIdResult?> HandleAsync(
        GetNotificationRoutingRuleByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var routingRule = await _notificationRoutingRuleRepository.GetByIdAsync(query.Id, cancellationToken);

        if (routingRule is null)
        {
            return null;
        }

        return new GetNotificationRoutingRuleByIdResult(
            routingRule.Id,
            routingRule.MessageType,
            routingRule.Channel,
            routingRule.ProviderId,
            routingRule.Priority,
            routingRule.IsActive,
            routingRule.CreatedAtUtc);
    }
}
