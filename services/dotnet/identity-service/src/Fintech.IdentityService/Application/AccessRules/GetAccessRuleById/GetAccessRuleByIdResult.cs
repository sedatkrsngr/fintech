using Fintech.IdentityService.Domain.Enums;

namespace Fintech.IdentityService.Application.AccessRules.GetAccessRuleById;

public sealed record GetAccessRuleByIdResult(
    Guid AccessRuleId,
    AccessRuleSubjectType SubjectType,
    Guid SubjectId,
    AccessRuleEffect Effect,
    string EndpointPattern,
    string? HttpVerb,
    bool IsActive,
    DateTime CreatedAtUtc);
