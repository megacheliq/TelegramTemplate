using Telegram.Bot.Types;
using TelegramTemplate.Application.Abstract.TelegramBot.Handlers;
using TelegramTemplate.Application.Abstract.TelegramBot.Sending;
using TelegramTemplate.Application.Constants;

namespace TelegramTemplate.Infrastructure.TelegramBot.Handlers;

public class MessageHandler : IMessageHandler
{
    private readonly ITelegramSendingService _telegramSendingService;

    public MessageHandler(ITelegramSendingService telegramSendingService)
    {
        _telegramSendingService = telegramSendingService;
    }
    
    /// <inheritdoc/>
    public async Task HandleHelpMessage(Message message, CancellationToken cancellationToken)
    {
        await _telegramSendingService.SendMessageAsync(message.Chat.Id, MessageTextConstants.HelpText,
            cancellationToken);
    }
}