using Fintech.NotificationService.Application.Abstractions;
using Fintech.NotificationService.Domain.Enums;

namespace Fintech.NotificationService.Infrastructure.Providers;

public sealed class NotificationProviderResolver : INotificationProviderResolver
{
    private readonly INotificationProviderRepository _notificationProviderRepository;
    private readonly IEnumerable<IEmailProvider> _emailProviders;
    private readonly IEnumerable<ISmsProvider> _smsProviders;
    private readonly IEnumerable<IRealtimeProvider> _realtimeProviders;

    public NotificationProviderResolver(
        INotificationProviderRepository notificationProviderRepository,
        IEnumerable<IEmailProvider> emailProviders,
        IEnumerable<ISmsProvider> smsProviders,
        IEnumerable<IRealtimeProvider> realtimeProviders)
    {
        _notificationProviderRepository = notificationProviderRepository;
        _emailProviders = emailProviders;
        _smsProviders = smsProviders;
        _realtimeProviders = realtimeProviders;
    }

    public async Task<IEmailProvider> ResolveEmailProviderAsync(Guid providerId, CancellationToken cancellationToken = default)
    {
        var provider = await _notificationProviderRepository.GetByIdAsync(providerId, cancellationToken)
            ?? throw new InvalidOperationException("Notification provider was not found.");

        if (provider.Channel != NotificationChannel.Email)
        {
            throw new InvalidOperationException("Notification provider channel does not match email delivery.");
        }

        if (provider.ProviderType is not NotificationProviderType.Mock and not NotificationProviderType.Smtp)
        {
            throw new NotSupportedException("Notification provider type is not supported for email delivery.");
        }

        return _emailProviders.FirstOrDefault(x => x.CanHandle(provider.ProviderKey.Value, provider.ProviderType))
            ?? throw new NotSupportedException("No email provider implementation matched the notification provider.");
    }

    public async Task<ISmsProvider> ResolveSmsProviderAsync(Guid providerId, CancellationToken cancellationToken = default)
    {
        var provider = await _notificationProviderRepository.GetByIdAsync(providerId, cancellationToken)
            ?? throw new InvalidOperationException("Notification provider was not found.");

        if (provider.Channel != NotificationChannel.Sms)
        {
            throw new InvalidOperationException("Notification provider channel does not match SMS delivery.");
        }

        if (provider.ProviderType != NotificationProviderType.Mock)
        {
            throw new NotSupportedException("Notification provider type is not supported for SMS delivery.");
        }

        return _smsProviders.FirstOrDefault(x => x.CanHandle(provider.ProviderKey.Value, provider.ProviderType))
            ?? throw new NotSupportedException("No SMS provider implementation matched the notification provider.");
    }

    public async Task<IRealtimeProvider> ResolveRealtimeProviderAsync(Guid providerId, CancellationToken cancellationToken = default)
    {
        var provider = await _notificationProviderRepository.GetByIdAsync(providerId, cancellationToken)
            ?? throw new InvalidOperationException("Notification provider was not found.");

        if (provider.Channel != NotificationChannel.Realtime)
        {
            throw new InvalidOperationException("Notification provider channel does not match realtime delivery.");
        }

        if (provider.ProviderType is not NotificationProviderType.Mock and not NotificationProviderType.SignalR)
        {
            throw new NotSupportedException("Notification provider type is not supported for realtime delivery.");
        }

        return _realtimeProviders.FirstOrDefault(x => x.CanHandle(provider.ProviderKey.Value, provider.ProviderType))
            ?? throw new NotSupportedException("No realtime provider implementation matched the notification provider.");
    }
}
