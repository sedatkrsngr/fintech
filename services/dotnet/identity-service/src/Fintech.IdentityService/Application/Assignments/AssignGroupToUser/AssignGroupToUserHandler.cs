using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.Assignments.AssignGroupToUser;

public sealed class AssignGroupToUserHandler
{
    private readonly IAuthorizationRepository _authorizationRepository;

    public AssignGroupToUserHandler(IAuthorizationRepository authorizationRepository)
    {
        _authorizationRepository = authorizationRepository;
    }

    public async Task HandleAsync(AssignGroupToUserCommand command, CancellationToken cancellationToken = default)
    {
        var userGroup = UserGroup.Create(UserId.From(command.UserId), GroupId.From(command.GroupId));
        await _authorizationRepository.AddUserGroupAsync(userGroup, cancellationToken);
    }
}
