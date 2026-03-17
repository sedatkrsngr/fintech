using Fintech.IdentityService.Domain.Enums;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Domain.Entities;

public sealed class User
{
    private User(UserId id, Email email, PasswordHash passwordHash)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        Status = UserStatus.Active;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public UserId Id { get; }

    public Email Email { get; private set; }

    public PasswordHash PasswordHash { get; private set; }

    public UserStatus Status { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public static User Create(Email email, PasswordHash passwordHash)
    {
        return new User(UserId.New(), email, passwordHash);
    }

    public void ChangeEmail(Email email)
    {
        Email = email;
    }

    public void ChangePasswordHash(PasswordHash passwordHash)
    {
        PasswordHash = passwordHash;
    }

    public void Suspend()
    {
        Status = UserStatus.Suspended;
    }
}
