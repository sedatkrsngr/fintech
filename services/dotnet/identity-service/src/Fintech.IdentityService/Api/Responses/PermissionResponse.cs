namespace Fintech.IdentityService.Api.Responses;

public sealed record PermissionResponse(Guid PermissionId, string Code, DateTime CreatedAtUtc);
