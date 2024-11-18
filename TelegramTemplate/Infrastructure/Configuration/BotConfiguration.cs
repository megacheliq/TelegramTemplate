using TelegramTemplate.Application.Abstract.Configuration;

namespace TelegramTemplate.Infrastructure.Configuration;

/// <summary>
/// Implementation of the bot configuration interface
/// </summary>
public class BotConfiguration : IBotConfiguration
{
    /// <inheritdoc />
    public string BotToken { get; init; } = default!;

    /// <inheritdoc />
    public string HostAddress { get; init; } = default!;

    /// <inheritdoc />
    public string Route { get; init; } = default!;

    /// <inheritdoc />
    public string SecretToken { get; init; } = default!;
        
    /// <inheritdoc />
    public string AppName { get; init; } = default!;
        
    /// <inheritdoc />
    public string BotName { get; init; } = default!;
    
    /// <summary>
    /// Name in appsettings
    /// </summary>
    public static readonly string Configuration = "BotConfiguration";
}