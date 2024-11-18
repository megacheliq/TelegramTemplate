using MediatR;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramTemplate.Application.Abstract.TelegramBot.Sending;
using TelegramTemplate.Application.UseCases.Commands.TelegramBotCommands;

namespace TelegramTemplate.Infrastructure.TelegramBot.Sending;

/// <inheritdoc />
public class TelegramSendingService : ITelegramSendingService
{
    private readonly IMediator _mediator;
    private readonly ILogger<TelegramSendingService> _logger;

    public TelegramSendingService(IMediator mediator, ILogger<TelegramSendingService> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    /// <inheritdoc />
    public async Task SendMessageAsync(long chatId, string message, CancellationToken cancellationToken,
        IReplyMarkup? replyMarkup = null,
        ParseMode parseMode = ParseMode.Html)
    {
        _logger.LogInformation($"Sending message to chat: {chatId}");
        await _mediator.Send(
            new SendMessageFromBotCommand
            { ChatId = chatId, Text = message, ParseMode = parseMode,
                ReplyMarkup = replyMarkup
            },
            cancellationToken);
    }
}