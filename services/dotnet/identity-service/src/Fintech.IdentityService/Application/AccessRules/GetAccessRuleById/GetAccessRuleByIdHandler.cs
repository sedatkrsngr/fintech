using Fintech.IdentityService.Application.Abstractions;

namespace Fintech.IdentityService.Application.AccessRules.GetAccessRuleById;

public sealed class GetAccessRuleByIdHandler
{
    private readonly IAuthorizationRepository _authorizationRepository;

    public GetAccessRuleByIdHandler(IAuthorizationRepository authorizationRepository)
    {
        _authorizationRepository = authorizationRepository;
    }

    public async Task<GetAccessRuleByIdResult?> HandleAsync(
        GetAccessRuleByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var accessRule = await _authorizationRepository.GetAccessRuleByIdAsync(query.AccessRuleId, cancellationToken);

        if (accessRule is null)
        {
            return null;
        }

        return new GetAccessRuleByIdResult(
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
