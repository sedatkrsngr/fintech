using Fintech.IdentityService.Application.Abstractions;

namespace Fintech.IdentityService.Application.Permissions.GetPermissionById;

public sealed class GetPermissionByIdHandler
{
    private readonly IAuthorizationRepository _authorizationRepository;

    public GetPermissionByIdHandler(IAuthorizationRepository authorizationRepository)
    {
        _authorizationRepository = authorizationRepository;
    }

    public async Task<GetPermissionByIdResult?> HandleAsync(GetPermissionByIdQuery query, CancellationToken cancellationToken = default)
    {
        var permission = await _authorizationRepository.GetPermissionByIdAsync(query.PermissionId, cancellationToken);

        return permission is null ? null : new GetPermissionByIdResult(permission.Id.Value, permission.Code.Value, permission.CreatedAtUtc);
    }
}
