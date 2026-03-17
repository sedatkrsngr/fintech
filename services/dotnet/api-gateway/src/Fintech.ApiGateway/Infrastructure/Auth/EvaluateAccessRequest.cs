namespace Fintech.ApiGateway.Infrastructure.Auth;

public sealed record EvaluateAccessRequest(
    int SubjectType,
    Guid SubjectId,
    string Path,
    string HttpMethod);
