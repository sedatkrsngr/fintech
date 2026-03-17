using Fintech.IdentityService.Domain.Enums;

namespace Fintech.IdentityService.Application.AccessRules.EvaluateAccess;

public sealed record EvaluateAccessCommand(
    AccessRuleSubjectType SubjectType,
    Guid SubjectId,
    string Path,
    string HttpMethod);
