using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.Permissions.CreatePermission;

public sealed class CreatePermissionHandler
{
    private readonly IAuthorizationRepository _authorizationRepository;

    public CreatePermissionHandler(IAuthorizationRepository authorizationRepository)
    {
        _authorizationRepository = authorizationRepository;
    }

    public async Task<CreatePermissionResult> HandleAsync(CreatePermissionCommand command, CancellationToken cancellationToken = default)
    {
        var permission = Permission.Create(PermissionCode.Create(command.Code));
        await _authorizationRepository.AddPermissionAsync(permission, cancellationToken);

        return new CreatePermissionResult(permission.Id.Value, permission.Code.Value, permission.CreatedAtUtc);
    }
}
