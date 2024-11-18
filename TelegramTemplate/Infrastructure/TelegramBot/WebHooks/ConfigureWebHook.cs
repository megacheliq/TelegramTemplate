using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TelegramTemplate.Application.Abstract.Configuration;

namespace TelegramTemplate.Infrastructure.TelegramBot.WebHooks;

public class ConfigureWebHook: IHostedService
{
    private readonly ILogger<ConfigureWebHook> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IBotConfiguration _botConfig;

    public ConfigureWebHook(
        ILogger<ConfigureWebHook> logger,
        IServiceProvider serviceProvider,
        IBotConfiguration botOptions)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _botConfig = botOptions;
    }

    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken)
    {

        using var scope = _serviceProvider.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        var webhookAddress = $"{_botConfig.HostAddress}{_botConfig.Route}";
        _logger.LogInformation("Setting webhook: {WebhookAddress}", webhookAddress);
        await botClient.SetWebhookAsync(
            url: webhookAddress,
            allowedUpdates: Array.Empty<UpdateType>(),
            secretToken: _botConfig.SecretToken,
            cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        _logger.LogInformation("Removing webhook");
        await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
    }
}