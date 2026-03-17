namespace Fintech.IdentityService.Application.Permissions.GetPermissionById;

public sealed record GetPermissionByIdResult(Guid PermissionId, string Code, DateTime CreatedAtUtc);
