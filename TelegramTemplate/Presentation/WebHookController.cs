using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using TelegramTemplate.Infrastructure.Attributes;
using TelegramTemplate.Infrastructure.TelegramBot.Handlers;

namespace TelegramTemplate.Presentation;

public class WebHookController: ControllerBase
{
    private readonly ITelegramBotClient _botClient;

    public WebHookController(ITelegramBotClient botClient)
    {
        _botClient = botClient;
    }
    
    /// <summary>
    /// Обрабатывает обновления от Telegram бота
    /// </summary>
    /// <param name="update">Обновление от Telegram бота</param>
    /// <param name="handleUpdateService">Сервис для обработки обновления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат обработки запроса</returns>
    [HttpPost]
    [ValidateTelegramBot]
    public async Task<IActionResult> Post(
        [FromBody] Telegram.Bot.Types.Update update,
        [FromServices] UpdateHandler handleUpdateService,
        CancellationToken cancellationToken)
    {
        await handleUpdateService.HandleUpdateAsync(_botClient, update, cancellationToken);
        return Ok();
    }
}