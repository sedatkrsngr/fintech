using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;

namespace Fintech.IdentityService.Application.Abstractions;

public interface IAuthLifecycleRepository
{
    Task AddRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);

    Task<RefreshToken?> GetRefreshTokenByIdAsync(
        RefreshTokenId refreshTokenId,
        CancellationToken cancellationToken = default);

    Task UpdateRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);

    Task AddPasswordResetTokenAsync(
        PasswordResetToken passwordResetToken,
        CancellationToken cancellationToken = default);

    Task<PasswordResetToken?> GetPasswordResetTokenByIdAsync(
        PasswordResetTokenId passwordResetTokenId,
        CancellationToken cancellationToken = default);

    Task UpdatePasswordResetTokenAsync(
        PasswordResetToken passwordResetToken,
        CancellationToken cancellationToken = default);

    Task AddEmailVerificationTokenAsync(
        EmailVerificationToken emailVerificationToken,
        CancellationToken cancellationToken = default);

    Task<EmailVerificationToken?> GetEmailVerificationTokenByIdAsync(
        EmailVerificationTokenId emailVerificationTokenId,
        CancellationToken cancellationToken = default);

    Task UpdateEmailVerificationTokenAsync(
        EmailVerificationToken emailVerificationToken,
        CancellationToken cancellationToken = default);
}
