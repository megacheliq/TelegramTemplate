using TelegramTemplate.Application.Abstract.TelegramBot.Polling;

namespace TelegramTemplate.Infrastructure.TelegramBot.Polling;

public class PollingService : PollingServiceBase<ReceiverService>
{
    public PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
        : base(serviceProvider, logger)
    {
    }
}