using Fintech.IdentityService.Domain.Enums;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Domain.Entities;

public sealed class User
{
    private User(UserId id, Email email)
    {
        Id = id;
        Email = email;
        Status = UserStatus.Active;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public UserId Id { get; }

    public Email Email { get; private set; }

    public UserStatus Status { get; private set; }

    public DateTime CreatedAtUtc { get; }

    public static User Create(Email email)
    {
        return new User(UserId.New(), email);
    }

    public void ChangeEmail(Email email)
    {
        Email = email;
    }

    public void Suspend()
    {
        Status = UserStatus.Suspended;
    }
}
