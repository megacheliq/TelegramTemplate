namespace TelegramTemplate.Application.Abstract.Configuration;

/// <summary>
/// Interface of client app configuration
/// </summary>
public interface IClientAppConfiguration
{
    /// <summary>
    /// Url of client
    /// </summary>
    string Address { get; }
}