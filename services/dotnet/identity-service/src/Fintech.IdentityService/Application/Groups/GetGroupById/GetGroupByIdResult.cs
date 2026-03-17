namespace Fintech.IdentityService.Application.Groups.GetGroupById;

public sealed record GetGroupByIdResult(Guid GroupId, string Name, DateTime CreatedAtUtc);
