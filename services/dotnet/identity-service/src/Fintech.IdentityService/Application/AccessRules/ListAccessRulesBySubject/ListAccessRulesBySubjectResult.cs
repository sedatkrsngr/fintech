using Fintech.IdentityService.Domain.Enums;

namespace Fintech.IdentityService.Application.AccessRules.ListAccessRulesBySubject;

public sealed record ListAccessRulesBySubjectResult(
    Guid AccessRuleId,
    AccessRuleSubjectType SubjectType,
    Guid SubjectId,
    AccessRuleEffect Effect,
    string EndpointPattern,
    string? HttpVerb,
    bool IsActive,
    DateTime CreatedAtUtc);
