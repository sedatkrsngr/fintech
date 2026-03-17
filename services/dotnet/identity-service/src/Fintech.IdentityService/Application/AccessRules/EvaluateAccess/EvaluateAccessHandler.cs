using System.Text.RegularExpressions;
using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.Enums;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.AccessRules.EvaluateAccess;

public sealed class EvaluateAccessHandler
{
    private readonly IAuthorizationRepository _authorizationRepository;

    public EvaluateAccessHandler(IAuthorizationRepository authorizationRepository)
    {
        _authorizationRepository = authorizationRepository;
    }

    public async Task<EvaluateAccessResult> HandleAsync(
        EvaluateAccessCommand command,
        CancellationToken cancellationToken = default)
    {
        var subjectRules = new List<AccessRule>();

        if (command.SubjectType == AccessRuleSubjectType.User)
        {
            var userId = UserId.From(command.SubjectId);

            subjectRules.AddRange(await _authorizationRepository.GetAccessRulesAsync(
                AccessRuleSubjectType.User,
                new[] { command.SubjectId },
                cancellationToken));

            var roleIds = await _authorizationRepository.GetRoleIdsByUserIdAsync(userId, cancellationToken);
            subjectRules.AddRange(await _authorizationRepository.GetAccessRulesAsync(
                AccessRuleSubjectType.Role,
                roleIds,
                cancellationToken));

            var groupIds = await _authorizationRepository.GetGroupIdsByUserIdAsync(userId, cancellationToken);
            subjectRules.AddRange(await _authorizationRepository.GetAccessRulesAsync(
                AccessRuleSubjectType.Group,
                groupIds,
                cancellationToken));
        }
        else
        {
            subjectRules.AddRange(await _authorizationRepository.GetAccessRulesAsync(
                command.SubjectType,
                new[] { command.SubjectId },
                cancellationToken));
        }

        if (subjectRules.Count == 0)
        {
            return new EvaluateAccessResult(true);
        }

        var matchingRules = subjectRules
            .Where(x => Matches(x, command.Path, command.HttpMethod))
            .ToList();

        if (matchingRules.Any(x => x.Effect == AccessRuleEffect.Deny))
        {
            return new EvaluateAccessResult(false);
        }

        if (matchingRules.Any(x => x.Effect == AccessRuleEffect.Allow))
        {
            return new EvaluateAccessResult(true);
        }

        return new EvaluateAccessResult(false);
    }

    private static bool Matches(AccessRule accessRule, string path, string httpMethod)
    {
        if (accessRule.HttpVerb is not null &&
            !string.Equals(accessRule.HttpVerb.Value, httpMethod, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        var normalizedPath = path.Trim().ToLowerInvariant();
        var pattern = "^" + Regex.Escape(accessRule.EndpointPattern.Value)
            .Replace("\\*", ".*") + "$";

        return Regex.IsMatch(normalizedPath, pattern, RegexOptions.IgnoreCase);
    }
}
