using Fintech.IdentityService.Domain.Enums;

namespace Fintech.IdentityService.Api.Requests;

public sealed record EvaluateAccessRequest(
    AccessRuleSubjectType SubjectType,
    Guid SubjectId,
    string Path,
    string HttpMethod);
