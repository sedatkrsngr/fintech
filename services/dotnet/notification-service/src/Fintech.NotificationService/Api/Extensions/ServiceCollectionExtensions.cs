using Fintech.NotificationService.Application.Providers.CreateNotificationProvider;
using Fintech.NotificationService.Application.Providers.GetNotificationProviderById;
using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Application.Deliveries.CreateEmailDelivery;
using Fintech.NotificationService.Application.Deliveries.CreateRealtimeDelivery;
using Fintech.NotificationService.Application.Deliveries.CreateSmsDelivery;
using Fintech.NotificationService.Application.Deliveries.GetNotificationDeliveryById;
using Fintech.NotificationService.Infrastructure.Persistence;
using Fintech.NotificationService.Infrastructure.Persistence.Repositories.EfCore;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Fintech.NotificationService.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotificationApi(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("NotificationDatabase")
            ?? throw new InvalidOperationException("Connection string 'NotificationDatabase' is missing.");

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        services.AddDbContext<NotificationDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<INotificationProviderRepository, NotificationProviderRepository>();
        services.AddScoped<INotificationDeliveryRepository, NotificationDeliveryRepository>();
        services.AddScoped<CreateNotificationProviderHandler>();
        services.AddScoped<GetNotificationProviderByIdHandler>();
        services.AddScoped<CreateEmailDeliveryHandler>();
        services.AddScoped<CreateSmsDeliveryHandler>();
        services.AddScoped<CreateRealtimeDeliveryHandler>();
        services.AddScoped<GetNotificationDeliveryByIdHandler>();

        return services;
    }
}
