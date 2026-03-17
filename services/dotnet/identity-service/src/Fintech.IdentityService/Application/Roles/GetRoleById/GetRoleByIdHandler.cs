using Fintech.IdentityService.Application.Abstractions;

namespace Fintech.IdentityService.Application.Roles.GetRoleById;

public sealed class GetRoleByIdHandler
{
    private readonly IAuthorizationRepository _authorizationRepository;

    public GetRoleByIdHandler(IAuthorizationRepository authorizationRepository)
    {
        _authorizationRepository = authorizationRepository;
    }

    public async Task<GetRoleByIdResult?> HandleAsync(GetRoleByIdQuery query, CancellationToken cancellationToken = default)
    {
        var role = await _authorizationRepository.GetRoleByIdAsync(query.RoleId, cancellationToken);

        return role is null ? null : new GetRoleByIdResult(role.Id.Value, role.Name.Value, role.CreatedAtUtc);
    }
}
