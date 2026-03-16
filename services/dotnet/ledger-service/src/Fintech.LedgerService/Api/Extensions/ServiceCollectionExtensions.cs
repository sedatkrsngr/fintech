using Fintech.LedgerService.Application.Abstractions;
using Fintech.LedgerService.Application.LedgerEntries.CreateLedgerEntry;
using Fintech.LedgerService.Application.LedgerEntries.GetLedgerEntryById;
using Fintech.LedgerService.Infrastructure.Persistence;
using Fintech.LedgerService.Infrastructure.Persistence.Repositories.EfCore;
using Microsoft.EntityFrameworkCore;

namespace Fintech.LedgerService.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("LedgerDatabase")
            ?? throw new InvalidOperationException("Connection string 'LedgerDatabase' is missing.");

        services.AddControllers();
        services.AddDbContext<LedgerDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<ILedgerEntryRepository, LedgerEntryRepository>();
        services.AddTransient<CreateLedgerEntryHandler>();
        services.AddTransient<GetLedgerEntryByIdHandler>();

        return services;
    }
}
