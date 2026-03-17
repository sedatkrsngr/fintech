namespace Fintech.IdentityService.Api.Requests;

public sealed record AssignGroupToUserRequest(Guid UserId, Guid GroupId);
