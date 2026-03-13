using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.Users.RegisterUser;

public sealed class RegisterUserHandler
{
    private readonly IUserRepository _userRepository;

    public RegisterUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<RegisterUserResult> HandleAsync(
        RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var email = Email.Create(command.Email);
        var user = User.Create(email);
        await _userRepository.AddAsync(user, cancellationToken);

        return new RegisterUserResult(user.Id.Value, user.Email.Value, user.CreatedAtUtc);
    }
}
