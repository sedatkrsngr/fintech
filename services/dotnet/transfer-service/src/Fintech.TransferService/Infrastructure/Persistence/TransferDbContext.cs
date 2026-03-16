using Fintech.TransferService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fintech.TransferService.Infrastructure.Persistence;

public sealed class TransferDbContext : DbContext
{
    public TransferDbContext(DbContextOptions<TransferDbContext> options)
        : base(options)
    {
    }

    public DbSet<Transfer> Transfers => Set<Transfer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransferDbContext).Assembly);
    }
}
