using Fintech.TransferService.Application.Abstractions;
using Fintech.TransferService.Application.Transfers.CreateTransfer;
using Fintech.TransferService.Application.Transfers.GetTransferById;
using Fintech.TransferService.Infrastructure.Persistence;
using Fintech.TransferService.Infrastructure.Persistence.Repositories.EfCore;
using Microsoft.EntityFrameworkCore;

namespace Fintech.TransferService.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TransferDatabase")
            ?? throw new InvalidOperationException("Connection string 'TransferDatabase' is missing.");

        services.AddControllers();
        services.AddDbContext<TransferDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<ITransferRepository, TransferRepository>();
        services.AddTransient<CreateTransferHandler>();
        services.AddTransient<GetTransferByIdHandler>();

        return services;
    }
}
