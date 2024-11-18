using TelegramTemplate.Application.Abstract.Configuration;

namespace TelegramTemplate.Infrastructure.Configuration;

/// <summary>
/// Implementation of client app configuration interface
/// </summary>
public class ClientAppConfiguration : IClientAppConfiguration
{
    /// <inheritdoc />
    public string Address { get; init; } = default!;
    
    /// <summary>
    /// Name in appsettings
    /// </summary>
    public static readonly string Configuration = "ClientApp";
}