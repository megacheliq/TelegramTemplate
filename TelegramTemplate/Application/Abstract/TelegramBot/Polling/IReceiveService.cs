namespace TelegramTemplate.Application.Abstract.TelegramBot.Polling;

public interface IReceiverService
{
    /// <summary>
    /// Receive
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    Task ReceiveAsync(CancellationToken stoppingToken);
}