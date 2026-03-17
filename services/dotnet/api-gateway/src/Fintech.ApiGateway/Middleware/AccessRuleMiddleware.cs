using Fintech.ApiGateway.Infrastructure.Auth;

namespace Fintech.ApiGateway.Middleware;

public sealed class AccessRuleMiddleware
{
    private static readonly string[] ProtectedPrefixes =
    {
        "/api/wallet",
        "/api/ledger",
        "/api/transfer",
        "/api/notification",
        "/hubs/notifications"
    };

    private readonly RequestDelegate _next;

    public AccessRuleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IHttpClientFactory httpClientFactory)
    {
        if (!RequiresAccessRuleEvaluation(context.Request.Path))
        {
            await _next(context);
            return;
        }

        if (context.User?.Identity?.IsAuthenticated != true)
        {
            await _next(context);
            return;
        }

        var subjectType = context.User.FindFirst("subject_type")?.Value;
        var subjectIdRaw = context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (!TryMapSubjectType(subjectType, out var mappedSubjectType) ||
            !Guid.TryParse(subjectIdRaw, out var subjectId))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return;
        }

        var client = httpClientFactory.CreateClient("identity-internal-auth");
        using var response = await client.PostAsJsonAsync(
            "/api/internal/auth/evaluate-access",
            new EvaluateAccessRequest(mappedSubjectType, subjectId, context.Request.Path, context.Request.Method),
            context.RequestAborted);

        if (!response.IsSuccessStatusCode)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return;
        }

        var payload = await response.Content.ReadFromJsonAsync<EvaluateAccessResponse>(cancellationToken: context.RequestAborted);

        if (payload?.IsAllowed != true)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return;
        }

        await _next(context);
    }

    private static bool RequiresAccessRuleEvaluation(PathString path)
    {
        var value = path.Value ?? string.Empty;

        return ProtectedPrefixes.Any(prefix => value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
    }

    private static bool TryMapSubjectType(string? subjectType, out int mappedSubjectType)
    {
        mappedSubjectType = 0;

        if (string.IsNullOrWhiteSpace(subjectType))
        {
            return false;
        }

        switch (subjectType.Trim().ToLowerInvariant())
        {
            case "user":
                mappedSubjectType = 1;
                return true;
            case "group":
                mappedSubjectType = 2;
                return true;
            case "role":
                mappedSubjectType = 3;
                return true;
            case "api_client":
                mappedSubjectType = 4;
                return true;
            default:
                return false;
        }
    }
}
