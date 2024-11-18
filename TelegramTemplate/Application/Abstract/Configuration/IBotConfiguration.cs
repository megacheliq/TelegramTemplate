namespace TelegramTemplate.Application.Abstract.Configuration;

/// <summary>
/// Interface for bot configuration in appsettings
/// </summary>
public interface IBotConfiguration
{
    /// <summary>
    /// Telegram-bot token
    /// </summary>
    string BotToken { get; }

    /// <summary>
    /// Host address
    /// </summary>
    string HostAddress { get; }

    /// <summary>
    /// Route for webhook
    /// </summary>
    string Route { get; }

    /// <summary>
    /// Secret token for webhook
    /// </summary>
    string SecretToken { get; }
        
    /// <summary>
    /// App name
    /// </summary>
    string AppName { get; }
        
    /// <summary>
    /// Bot name
    /// </summary>
    string BotName { get; }
}