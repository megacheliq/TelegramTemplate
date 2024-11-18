namespace TelegramTemplate.Application.Abstract.TelegramBot.WebHooks;

public interface ISwapTechnology
{
    /// <summary>
    /// Stop webhook
    /// </summary>
    /// <param name="cancellationToken">cancellationToken</param>
    Task StopWebHook(CancellationToken cancellationToken);
}