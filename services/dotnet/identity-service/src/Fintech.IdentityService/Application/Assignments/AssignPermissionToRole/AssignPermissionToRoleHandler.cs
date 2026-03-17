using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.Assignments.AssignPermissionToRole;

public sealed class AssignPermissionToRoleHandler
{
    private readonly IAuthorizationRepository _authorizationRepository;

    public AssignPermissionToRoleHandler(IAuthorizationRepository authorizationRepository)
    {
        _authorizationRepository = authorizationRepository;
    }

    public async Task HandleAsync(AssignPermissionToRoleCommand command, CancellationToken cancellationToken = default)
    {
        var rolePermission = RolePermission.Create(RoleId.From(command.RoleId), PermissionId.From(command.PermissionId));
        await _authorizationRepository.AddRolePermissionAsync(rolePermission, cancellationToken);
    }
}
