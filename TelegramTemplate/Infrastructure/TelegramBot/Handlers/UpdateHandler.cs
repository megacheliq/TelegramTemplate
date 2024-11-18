using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using TelegramTemplate.Application.Abstract.MessageLogic;
using TelegramTemplate.Application.Abstract.TelegramBot.Handlers;
using TelegramTemplate.Application.Abstract.UserLogic;

namespace TelegramTemplate.Infrastructure.TelegramBot.Handlers;

public class UpdateHandler : IUpdateHandler
{
    private readonly ILogger<UpdateHandler> _logger;
    private readonly IMessageHandler _messageHandler;
    private readonly IUserLogicService _userLogicService;
    private readonly IMessageLogicService _messageLogicService;

    public UpdateHandler(ILogger<UpdateHandler> logger,
        IMessageHandler messageHandler,
        IUserLogicService userLogicService,
        IMessageLogicService messageLogicService)
    {
        _logger = logger;
        _messageHandler = messageHandler;
        _userLogicService = userLogicService;
        _messageLogicService = messageLogicService;
    }
    
    /// <inheritdoc/>
    public async Task HandleUpdateAsync(ITelegramBotClient _, Update update, CancellationToken cancellationToken)
    {
        var handler = update switch
        {
            { Message: { } message } => BotOnMessageReceiveOrEdit(message, cancellationToken),
            { EditedMessage: { } message } => BotOnMessageReceiveOrEdit(message, cancellationToken, true),
            { CallbackQuery: { } callbackQuery } => UnknownUpdateHandlerAsync(update, cancellationToken),
            _ => UnknownUpdateHandlerAsync(update, cancellationToken)
        };

        await handler;
    }
    
    /// <summary>
    /// Обработка исключений polling'a
    /// </summary>
    /// <param name="botClient">Бот</param>
    /// <param name="exception">Исключение</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException =>
                $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        _logger.LogInformation("HandleError: {ErrorMessage}", errorMessage);

        // Cooldown in case of network connection error
        if (exception is RequestException)
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
    }

    /// <summary>
    /// Обработка не имплементированного / неизвестного события
    /// </summary>
    /// <param name="update">Событие</param>
    /// <param name="cancellationToken">Токен отмены</param>
    private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }

    private Task BotOnMessageReceiveOrEdit(Message message, CancellationToken cancellationToken, bool edit = false)
    {
        return HandleMessage(
            message,
            cancellationToken,
            edit ? $"Received edited message: {message.Text}" : $"Received new message: {message.Text}",
            edit
        );
    }

    private async Task HandleMessage(Message message, CancellationToken cancellationToken, string logMessage, bool edit)
    {
        await _userLogicService.EnsureUserExists(message.Chat.Id, message.Chat.Username, cancellationToken);

        await (edit 
            ? _messageLogicService.UpdateMessageAsync(message.Id, message.Text, cancellationToken) 
            : _messageLogicService.CreateMessageAsync(message.Chat.Id, message.Id, message.Text, cancellationToken));
        
        if (message.Text is not { })
            return;

        _logger.LogInformation(logMessage, message.Text);

        await (message.Text.Split(' ')[0] switch
        {
            _ => _messageHandler.HandleHelpMessage(message, cancellationToken),
        });
    }

    /// <inheritdoc />
    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}