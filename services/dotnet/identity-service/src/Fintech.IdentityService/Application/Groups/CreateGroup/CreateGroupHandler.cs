using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.Groups.CreateGroup;

public sealed class CreateGroupHandler
{
    private readonly IAuthorizationRepository _authorizationRepository;

    public CreateGroupHandler(IAuthorizationRepository authorizationRepository)
    {
        _authorizationRepository = authorizationRepository;
    }

    public async Task<CreateGroupResult> HandleAsync(CreateGroupCommand command, CancellationToken cancellationToken = default)
    {
        var group = Group.Create(GroupName.Create(command.Name));
        await _authorizationRepository.AddGroupAsync(group, cancellationToken);

        return new CreateGroupResult(group.Id.Value, group.Name.Value, group.CreatedAtUtc);
    }
}
