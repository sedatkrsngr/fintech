using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.Assignments.AssignRoleToUser;

public sealed class AssignRoleToUserHandler
{
    private readonly IAuthorizationRepository _authorizationRepository;

    public AssignRoleToUserHandler(IAuthorizationRepository authorizationRepository)
    {
        _authorizationRepository = authorizationRepository;
    }

    public async Task HandleAsync(AssignRoleToUserCommand command, CancellationToken cancellationToken = default)
    {
        var userRole = UserRole.Create(UserId.From(command.UserId), RoleId.From(command.RoleId));
        await _authorizationRepository.AddUserRoleAsync(userRole, cancellationToken);
    }
}
