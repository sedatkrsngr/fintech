namespace Fintech.IdentityService.Application.Groups.CreateGroup;

public sealed record CreateGroupResult(Guid GroupId, string Name, DateTime CreatedAtUtc);
