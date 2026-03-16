using Fintech.WalletService.Application.Abstractions;
using Fintech.WalletService.Application.Wallets.CreateWallet;
using Fintech.WalletService.Application.Wallets.GetWalletById;
using Fintech.WalletService.Infrastructure.Persistence;
using Fintech.WalletService.Infrastructure.Persistence.Repositories.EfCore;
using Microsoft.EntityFrameworkCore;

namespace Fintech.WalletService.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("WalletDatabase")
            ?? throw new InvalidOperationException("Connection string 'WalletDatabase' is missing.");

        services.AddControllers();
        services.AddDbContext<WalletDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddTransient<CreateWalletHandler>();
        services.AddTransient<GetWalletByIdHandler>();

        return services;
    }
}
