namespace Fintech.NotificationService.Application.Abstractions;

public interface INotificationProviderResolver
{
    Task<IEmailProvider> ResolveEmailProviderAsync(Guid providerId, CancellationToken cancellationToken = default);

    Task<ISmsProvider> ResolveSmsProviderAsync(Guid providerId, CancellationToken cancellationToken = default);

    Task<IRealtimeProvider> ResolveRealtimeProviderAsync(Guid providerId, CancellationToken cancellationToken = default);
}
