using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramTemplate.Application.Abstract.TelegramBot.Sending;

/// <summary>
/// Service for sending messages from bot in telegram
/// </summary>
public interface ITelegramSendingService
{
    /// <summary>
    /// Send message
    /// </summary>
    /// <param name="chatId">Chat identifier</param>
    /// <param name="message">Message text</param>
    /// <param name="cancellationToken">cancellationToken</param>
    /// <param name="replyMarkup">Buttons</param>
    /// <param name="parseMode">Parse mode</param>
    Task SendMessageAsync(long chatId, string message, CancellationToken cancellationToken,
        IReplyMarkup? replyMarkup = null,
        ParseMode parseMode = ParseMode.Html);

}