namespace Fintech.IdentityService.Application.Permissions.CreatePermission;

public sealed record CreatePermissionResult(Guid PermissionId, string Code, DateTime CreatedAtUtc);
