using MediatR;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramTemplate.Application.UseCases.Commands.TelegramBotCommands;

public class SendMessageFromBotCommand : IRequest
{
    public required long ChatId { get; set; }

    public required string Text { get; set; } = default!;

    public required ParseMode ParseMode { get; set; }
    
    public IReplyMarkup? ReplyMarkup { get; set; }
}

public class SendMessageFromBotCommandHandler : IRequestHandler<SendMessageFromBotCommand>
{
    private ITelegramBotClient _botClient { get; }

    public SendMessageFromBotCommandHandler(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }

    public async Task Handle(SendMessageFromBotCommand request, CancellationToken cancellationToken)
    {
        
        await _botClient.SendTextMessageAsync(
            chatId: request.ChatId,
            text: request.Text,
            parseMode: request.ParseMode,
            replyMarkup: request.ReplyMarkup);
    }
}