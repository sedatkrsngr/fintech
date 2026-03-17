using Fintech.IdentityService.Domain.Enums;

namespace Fintech.IdentityService.Application.AccessRules.ListAccessRulesBySubject;

public sealed record ListAccessRulesBySubjectQuery(
    AccessRuleSubjectType SubjectType,
    Guid SubjectId);
