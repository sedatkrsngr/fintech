using Fintech.IdentityService.Application.Abstractions;

namespace Fintech.IdentityService.Application.AccessRules.ListAccessRulesBySubject;

public sealed class ListAccessRulesBySubjectHandler
{
    private readonly IAuthorizationRepository _authorizationRepository;

    public ListAccessRulesBySubjectHandler(IAuthorizationRepository authorizationRepository)
    {
        _authorizationRepository = authorizationRepository;
    }

    public async Task<IReadOnlyList<ListAccessRulesBySubjectResult>> HandleAsync(
        ListAccessRulesBySubjectQuery query,
        CancellationToken cancellationToken = default)
    {
        var accessRules = await _authorizationRepository.GetAccessRulesAsync(
            query.SubjectType,
            new[] { query.SubjectId },
            cancellationToken);

        return accessRules
            .OrderByDescending(x => x.CreatedAtUtc)
            .Select(x => new ListAccessRulesBySubjectResult(
                x.Id.Value,
                x.SubjectType,
                x.SubjectId,
                x.Effect,
                x.EndpointPattern.Value,
                x.HttpVerb?.Value,
                x.IsActive,
                x.CreatedAtUtc))
            .ToList();
    }
}
