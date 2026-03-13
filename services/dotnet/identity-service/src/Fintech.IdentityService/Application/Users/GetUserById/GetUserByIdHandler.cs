using Fintech.IdentityService.Application.Abstractions;

namespace Fintech.IdentityService.Application.Users.GetUserById;

public sealed class GetUserByIdHandler
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserByIdResult?> HandleAsync(
        GetUserByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId, cancellationToken);

        if (user is null)
        {
            return null;
        }

        return new GetUserByIdResult(
            user.Id.Value,
            user.Email.Value,
            user.Status.ToString(),
            user.CreatedAtUtc);
    }
}
