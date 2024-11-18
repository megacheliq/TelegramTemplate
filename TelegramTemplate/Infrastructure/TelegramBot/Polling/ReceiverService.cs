using Telegram.Bot;
using TelegramTemplate.Application.Abstract.TelegramBot.Polling;
using TelegramTemplate.Application.Abstract.TelegramBot.WebHooks;
using TelegramTemplate.Infrastructure.TelegramBot.Handlers;

namespace TelegramTemplate.Infrastructure.TelegramBot.Polling;

public class ReceiverService : ReceiverServiceBase<UpdateHandler>
{
    public ReceiverService(
        ITelegramBotClient botClient,
        UpdateHandler updateHandler,
        ILogger<IReceiverService> logger,
        ISwapTechnology swapTechnology)
        : base(botClient, updateHandler, logger, swapTechnology)
    {
    }
}