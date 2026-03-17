using Fintech.IdentityService.Domain.Enums;

namespace Fintech.IdentityService.Api.Responses;

public sealed record AccessRuleResponse(
    Guid AccessRuleId,
    AccessRuleSubjectType SubjectType,
    Guid SubjectId,
    AccessRuleEffect Effect,
    string EndpointPattern,
    string? HttpVerb,
    bool IsActive,
    DateTime CreatedAtUtc);
