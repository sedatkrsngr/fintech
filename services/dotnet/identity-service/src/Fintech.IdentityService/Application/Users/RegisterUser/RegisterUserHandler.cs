using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.Users.RegisterUser;

public sealed class RegisterUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashingService _passwordHashingService;

    public RegisterUserHandler(
        IUserRepository userRepository,
        IPasswordHashingService passwordHashingService)
    {
        _userRepository = userRepository;
        _passwordHashingService = passwordHashingService;
    }

    public async Task<RegisterUserResult> HandleAsync(
        RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var email = Email.Create(command.Email);
        var passwordHash = PasswordHash.Create(_passwordHashingService.HashPassword(command.Password));
        var user = User.Create(email, passwordHash);
        await _userRepository.AddAsync(user, cancellationToken);

        return new RegisterUserResult(user.Id.Value, user.Email.Value, user.CreatedAtUtc);
    }
}
