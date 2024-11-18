using Telegram.Bot;
using TelegramTemplate.Application.Abstract.TelegramBot.WebHooks;

namespace TelegramTemplate.Infrastructure.TelegramBot.WebHooks;

public class SwapTechnology : ISwapTechnology
{
    private readonly ILogger<SwapTechnology> _logger;
    private readonly IServiceProvider _serviceProvider;

    public SwapTechnology(
        ILogger<SwapTechnology> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc/>
    public async Task StopWebHook(CancellationToken cancellationToken)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

            _logger.LogInformation("Removing webhook");
            await botClient.DeleteWebhook(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error removing webhook: {Message}", ex.Message);
        }
    }
}