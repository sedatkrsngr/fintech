using Fintech.IdentityService.Application.Abstractions;

namespace Fintech.IdentityService.Application.Groups.GetGroupById;

public sealed class GetGroupByIdHandler
{
    private readonly IAuthorizationRepository _authorizationRepository;

    public GetGroupByIdHandler(IAuthorizationRepository authorizationRepository)
    {
        _authorizationRepository = authorizationRepository;
    }

    public async Task<GetGroupByIdResult?> HandleAsync(GetGroupByIdQuery query, CancellationToken cancellationToken = default)
    {
        var group = await _authorizationRepository.GetGroupByIdAsync(query.GroupId, cancellationToken);

        return group is null ? null : new GetGroupByIdResult(group.Id.Value, group.Name.Value, group.CreatedAtUtc);
    }
}
