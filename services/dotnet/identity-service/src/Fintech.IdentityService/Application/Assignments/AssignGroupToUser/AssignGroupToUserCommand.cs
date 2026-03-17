namespace Fintech.IdentityService.Application.Assignments.AssignGroupToUser;

public sealed record AssignGroupToUserCommand(Guid UserId, Guid GroupId);
