using Fintech.IdentityService.Domain.Enums;

namespace Fintech.IdentityService.Api.Requests;

public sealed record CreateAccessRuleRequest(
    AccessRuleSubjectType SubjectType,
    Guid SubjectId,
    AccessRuleEffect Effect,
    string EndpointPattern,
    string? HttpVerb);
