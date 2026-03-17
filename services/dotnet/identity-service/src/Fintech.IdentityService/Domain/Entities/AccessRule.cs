using Fintech.IdentityService.Domain.Enums;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Domain.Entities;

public sealed class AccessRule
{
    private AccessRule(
        AccessRuleId id,
        AccessRuleSubjectType subjectType,
        Guid subjectId,
        AccessRuleEffect effect,
        EndpointPattern endpointPattern,
        HttpVerb? httpVerb)
    {
        if (subjectId == Guid.Empty)
        {
            throw new ArgumentException("Subject id cannot be empty.", nameof(subjectId));
        }

        Id = id;
        SubjectType = subjectType;
        SubjectId = subjectId;
        Effect = effect;
        EndpointPattern = endpointPattern;
        HttpVerb = httpVerb;
        IsActive = true;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public AccessRuleId Id { get; }

    public AccessRuleSubjectType SubjectType { get; }

    public Guid SubjectId { get; }

    public AccessRuleEffect Effect { get; private set; }

    public EndpointPattern EndpointPattern { get; private set; }

    public HttpVerb? HttpVerb { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public static AccessRule Create(
        AccessRuleSubjectType subjectType,
        Guid subjectId,
        AccessRuleEffect effect,
        EndpointPattern endpointPattern,
        HttpVerb? httpVerb = null)
    {
        return new AccessRule(
            AccessRuleId.New(),
            subjectType,
            subjectId,
            effect,
            endpointPattern,
            httpVerb);
    }

    public void UpdateScope(
        AccessRuleEffect effect,
        EndpointPattern endpointPattern,
        HttpVerb? httpVerb = null)
    {
        Effect = effect;
        EndpointPattern = endpointPattern;
        HttpVerb = httpVerb;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
