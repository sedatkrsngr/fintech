using Fintech.NotificationService.Application.Providers.CreateNotificationProvider;
using Fintech.NotificationService.Application.Providers.GetNotificationProviderById;
using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Application.Deliveries.CreateEmailDelivery;
using Fintech.NotificationService.Application.Deliveries.CreateRealtimeDelivery;
using Fintech.NotificationService.Application.Deliveries.CreateSmsDelivery;
using Fintech.NotificationService.Application.Deliveries.GetNotificationDeliveryById;
using Fintech.NotificationService.Application.Routing.CreateNotificationRoutingRule;
using Fintech.NotificationService.Application.Routing.GetNotificationRoutingRuleById;
using Fintech.NotificationService.Configuration;
using Fintech.NotificationService.Infrastructure.Persistence;
using Fintech.NotificationService.Infrastructure.Persistence.Repositories.EfCore;
using Fintech.NotificationService.Infrastructure.Providers.Email;
using Fintech.NotificationService.Infrastructure.Providers;
using Fintech.NotificationService.Infrastructure.Providers.Realtime;
using Fintech.NotificationService.Infrastructure.Providers.Sms;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Fintech.NotificationService.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotificationApi(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("NotificationDatabase")
            ?? throw new InvalidOperationException("Connection string 'NotificationDatabase' is missing.");

        services.Configure<MailhogOptions>(configuration.GetSection("Mailhog"));
        services.Configure<MailpitOptions>(configuration.GetSection("Mailpit"));

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        services.AddSignalR();
        services.AddDbContext<NotificationDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<INotificationProviderRepository, NotificationProviderRepository>();
        services.AddScoped<INotificationDeliveryRepository, NotificationDeliveryRepository>();
        services.AddScoped<INotificationRoutingRuleRepository, NotificationRoutingRuleRepository>();
        services.AddScoped<IEmailProvider, MockEmailProvider>();
        services.AddScoped<IEmailProvider, MailhogEmailProvider>();
        services.AddScoped<IEmailProvider, MailpitEmailProvider>();
        services.AddScoped<ISmsProvider, MockSmsProvider>();
        services.AddScoped<IRealtimeProvider, MockRealtimeProvider>();
        services.AddScoped<IRealtimeProvider, SignalRRealtimeProvider>();
        services.AddScoped<INotificationProviderResolver, NotificationProviderResolver>();
        services.AddScoped<CreateNotificationProviderHandler>();
        services.AddScoped<GetNotificationProviderByIdHandler>();
        services.AddScoped<CreateEmailDeliveryHandler>();
        services.AddScoped<CreateSmsDeliveryHandler>();
        services.AddScoped<CreateRealtimeDeliveryHandler>();
        services.AddScoped<GetNotificationDeliveryByIdHandler>();
        services.AddScoped<CreateNotificationRoutingRuleHandler>();
        services.AddScoped<GetNotificationRoutingRuleByIdHandler>();

        return services;
    }
}
