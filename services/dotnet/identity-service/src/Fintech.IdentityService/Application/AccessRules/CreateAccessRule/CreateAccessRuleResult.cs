using Fintech.IdentityService.Domain.Enums;

namespace Fintech.IdentityService.Application.AccessRules.CreateAccessRule;

public sealed record CreateAccessRuleResult(
    Guid AccessRuleId,
    AccessRuleSubjectType SubjectType,
    Guid SubjectId,
    AccessRuleEffect Effect,
    string EndpointPattern,
    string? HttpVerb,
    bool IsActive,
    DateTime CreatedAtUtc);
