using Telegram.Bot.Types;

namespace TelegramTemplate.Application.Abstract.TelegramBot.Handlers;

/// <summary>
/// Service for handling received messages
/// </summary>
public interface IMessageHandler
{
    /// <summary>
    ///  /help command
    /// </summary>
    /// <param name="message">Message</param>
    /// <param name="cancellationToken">cancellationToken</param>
    Task HandleHelpMessage(Message message, CancellationToken cancellationToken);
}