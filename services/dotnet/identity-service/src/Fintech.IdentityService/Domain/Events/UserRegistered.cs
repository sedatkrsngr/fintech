using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Domain.Events;

public sealed record UserRegistered(UserId UserId, Email Email, DateTime OccurredAtUtc);
