using Fintech.IdentityService.Application.Abstractions;
using Fintech.IdentityService.Domain.Entities;
using Fintech.IdentityService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Fintech.IdentityService.Infrastructure.Persistence.Repositories.EfCore;

public sealed class AuthLifecycleRepository : IAuthLifecycleRepository
{
    private readonly IdentityDbContext _dbContext;

    public AuthLifecycleRepository(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<RefreshToken?> GetRefreshTokenByIdAsync(
        RefreshTokenId refreshTokenId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.RefreshTokens
            .FirstOrDefaultAsync(x => x.Id == refreshTokenId, cancellationToken);
    }

    public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        _dbContext.RefreshTokens.Update(refreshToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddPasswordResetTokenAsync(
        PasswordResetToken passwordResetToken,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.PasswordResetTokens.AddAsync(passwordResetToken, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<PasswordResetToken?> GetPasswordResetTokenByIdAsync(
        PasswordResetTokenId passwordResetTokenId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.PasswordResetTokens
            .FirstOrDefaultAsync(x => x.Id == passwordResetTokenId, cancellationToken);
    }

    public async Task UpdatePasswordResetTokenAsync(
        PasswordResetToken passwordResetToken,
        CancellationToken cancellationToken = default)
    {
        _dbContext.PasswordResetTokens.Update(passwordResetToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddEmailVerificationTokenAsync(
        EmailVerificationToken emailVerificationToken,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.EmailVerificationTokens.AddAsync(emailVerificationToken, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<EmailVerificationToken?> GetEmailVerificationTokenByIdAsync(
        EmailVerificationTokenId emailVerificationTokenId,
        CancellationToken cancellationToken = default)
    {
        return _dbContext.EmailVerificationTokens
            .FirstOrDefaultAsync(x => x.Id == emailVerificationTokenId, cancellationToken);
    }

    public async Task UpdateEmailVerificationTokenAsync(
        EmailVerificationToken emailVerificationToken,
        CancellationToken cancellationToken = default)
    {
        _dbContext.EmailVerificationTokens.Update(emailVerificationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
