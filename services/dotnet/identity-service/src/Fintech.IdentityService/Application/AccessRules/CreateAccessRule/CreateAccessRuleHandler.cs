using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.AccessRules.CreateAccessRule;

public sealed class CreateAccessRuleHandler
{
    private readonly IAuthorizationRepository _authorizationRepository;

    public CreateAccessRuleHandler(IAuthorizationRepository authorizationRepository)
    {
        _authorizationRepository = authorizationRepository;
    }

    public async Task<CreateAccessRuleResult> HandleAsync(
        CreateAccessRuleCommand command,
        CancellationToken cancellationToken = default)
    {
        var httpVerb = string.IsNullOrWhiteSpace(command.HttpVerb)
            ? null
            : HttpVerb.Create(command.HttpVerb);

        var accessRule = AccessRule.Create(
            command.SubjectType,
            command.SubjectId,
            command.Effect,
            EndpointPattern.Create(command.EndpointPattern),
            httpVerb);

        await _authorizationRepository.AddAccessRuleAsync(accessRule, cancellationToken);

        return new CreateAccessRuleResult(
            accessRule.Id.Value,
            accessRule.SubjectType,
            accessRule.SubjectId,
            accessRule.Effect,
            accessRule.EndpointPattern.Value,
            accessRule.HttpVerb?.Value,
            accessRule.IsActive,
            accessRule.CreatedAtUtc);
    }
}
