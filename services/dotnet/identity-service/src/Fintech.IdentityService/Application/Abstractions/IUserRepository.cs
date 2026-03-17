using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.Abstractions;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken = default);

    Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken = default);

    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
}
