using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.Roles.CreateRole;

public sealed class CreateRoleHandler
{
    private readonly IAuthorizationRepository _authorizationRepository;

    public CreateRoleHandler(IAuthorizationRepository authorizationRepository)
    {
        _authorizationRepository = authorizationRepository;
    }

    public async Task<CreateRoleResult> HandleAsync(CreateRoleCommand command, CancellationToken cancellationToken = default)
    {
        var role = Role.Create(RoleName.Create(command.Name));
        await _authorizationRepository.AddRoleAsync(role, cancellationToken);

        return new CreateRoleResult(role.Id.Value, role.Name.Value, role.CreatedAtUtc);
    }
}
